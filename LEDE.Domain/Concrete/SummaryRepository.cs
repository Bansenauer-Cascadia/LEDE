using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;
using System.Web.Mvc; 

namespace LEDE.Domain.Concrete
{
    public class SummaryRepository : ISummaryRepository
    {
        private DbContext db;

        public SummaryRepository(DbContext context)
        {
            this.db = context;
        }

        public SummaryRepository()
        {
            this.db = new DbContext();
        }

        public SeminarSummary getCohortTotals(int cohortID, int userID)
        {
            return null;
        }

        public StudentSummary getStudentTotals(int cohortID, int userID)
        {
            IEnumerable<RatingQuery> UserTotals = db.Database.SqlQuery<RatingQuery>("facultyview @p0", new object[]{cohortID}).
                Where(ut=> ut.UserID == userID); 

            var CombinedTotals = UserTotals.Select(u => new { Total = u.CScore + u.PScore + u.SScore });

            var CombinedCounts = UserTotals.Select(u => new { Count = u.OneCount + u.TwoCount + u.ThreeCount });

            StudentSummary model = new StudentSummary()
            {
                RatingsList = UserTotals.ToList(),
                MaxTotal = CombinedTotals.Any() ? (CombinedTotals.Max(t => t.Total) ?? 0) : 0,
                MaxCount = CombinedCounts.Any() ? CombinedCounts.Max(c => c.Count) : 0
            };

            return model;
        }

        public IEnumerable<ProgramCohort> getCohorts()
        {
            return db.ProgramCohorts;
        }


        public SpreadsheetModel getSpreadsheetTable(int ProgramCohortID, int UserID)
        {
            int programID = db.ProgramCohorts.Find(ProgramCohortID).ProgramID;
            SpreadsheetModel model = new SpreadsheetModel()
            {
                TableBody = new SpreadsheetTable()
                {
                    Rows = new List<SpreadsheetRow>()
                },
                CohortTasks = db.Tasks.Where(t=> t.Seminar.ProgramID == programID),
            };
            IEnumerable<CoreTopic> CoreTopics = db.CoreTopics.Where(c=> c.Seminar.ProgramID == programID);
            IEnumerable<CoreTopicScore> userScores = db.Database.SqlQuery<CoreTopicScore>(
                "SELECT * FROM coretopicscores WHERE userid = @p0 AND programcohortid = @p1", new object[] {UserID, ProgramCohortID}); 

            foreach(CoreTopic topic in CoreTopics) 
            {
                IEnumerable<CoreTopicScore> topicScores = userScores.Where(s=> s.CoreTopicID == topic.CoreTopicID); 
                SpreadsheetRow row = new SpreadsheetRow() { Scores = new List<CoreTopicScore>(), CoreTopic = topic.CoreTopicNum + " " +
                topic.CoreTopicDesc};

                foreach(Task task in model.CohortTasks)
                {
                    CoreTopicScore score = topicScores.FirstOrDefault(s => s.TaskID == task.TaskID);
                    if (score != null)
                        row.Scores.Add(score);
                    else
                        row.Scores.Add(new CoreTopicScore()); 
                }
                model.TableBody.Rows.Add(row); 
            }

            return model; 
        }


        public SummaryModel getSummaryCohorts(int UserID, int? SelectedCohortID)
        {
            var userCohorts = db.ProgramCohorts.Select(e =>
                    new {Value = e.ProgramCohortID, Text = e.Program.ProgramTitle 
                        + " " + e.AcademicYear }); 
            SummaryModel model = new SummaryModel()
            {
                ProgramCohorts = new SelectList(userCohorts, "Value", "Text", SelectedCohortID ?? userCohorts.First().Value),
                ProgramCohortID = SelectedCohortID ?? userCohorts.First().Value
            };

            return model; 
        }

        public void getSummaryCandidates(SummaryModel model)
        {
            var candidates = db.CandidateEnrollments.Where(e => e.ProgramCohortID == model.ProgramCohortID).Select(e =>
                new { Value = e.UserID, Text = e.User.LastName + ", " +e.User.FirstName}).OrderBy(c=> c.Text);
            model.Candidates = new SelectList(candidates, "Value", "Text");
        }
    }
}