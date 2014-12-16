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

        public IEnumerable<RatingQuery> getCohortTotals(int cohortID)
        {
            IEnumerable<RatingQuery> model = db.Database.SqlQuery<RatingQuery>("facultycohortview @p0", new object[] { cohortID }).ToList();

            var CombinedTotals = model.Select(u => new { Total = (u.CScore ?? 0) + (u.PScore ?? 0) + (u.SScore ?? 0) });
            var CombinedCounts = model.Select(u => new { Count = u.OneCount + u.TwoCount + u.ThreeCount });

            int maxTotal = CombinedTotals.Any() ? (CombinedTotals.Max(t => t.Total)) : 0;
            int maxCount = CombinedCounts.Any() ? CombinedCounts.Max(c => c.Count) : 0;

            foreach (RatingQuery total in model)
            {
                total.CPercentage = 100 * (total.CScore ?? 0) / maxTotal;
                total.SPercentage = 100 * (total.SScore ?? 0) / maxTotal;
                total.PPercentage = 100 * (total.PScore ?? 0) / maxTotal;

                total.OnePercentage = 100 * total.OneCount / maxCount;
                total.TwoPercentage = 100 * total.TwoCount / maxCount;
                total.ThreePercentage = 100 * total.ThreeCount / maxCount;
            }

            return model; 
        }

        public StudentSummary getStudentTotals(int cohortID, int userID)
        {
            StudentSummary model = new StudentSummary()
            {
                RatingsList = db.Database.SqlQuery<RatingQuery>("facultyview @p0", new object[]{cohortID})
                .Where(ut=> ut.UserID == userID).ToList() 
            };           

            var CombinedTotals = model.RatingsList.Select(u => new { Total = (u.CScore ?? 0) + (u.PScore ?? 0) + (u.SScore ?? 0) });
            var CombinedCounts = model.RatingsList.Select(u => new { Count = u.OneCount + u.TwoCount + u.ThreeCount });
            
            model.MaxTotal = CombinedTotals.Any() ? (CombinedTotals.Max(t => t.Total)) : 0;
            model.MaxCount = CombinedCounts.Any() ? CombinedCounts.Max(c => c.Count) : 0;

            return model;
        }

        public IEnumerable<ProgramCohort> getCohorts(int FacultyID)
        {
            if (FacultyID == 0)
                return db.ProgramCohorts;
            else
                return db.CohortEnrollments.Where(e => e.UserID == FacultyID).Select(e => e.ProgramCohort);
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
            IEnumerable<CoreTopic> ProgramCoreTopics = db.CoreTopics.Where(c=> c.Seminar.ProgramID == programID);
            IEnumerable<CoreTopicScore> userScores = db.Database.SqlQuery<CoreTopicScore>(
                "SELECT * FROM CoreTopicScoresByTask WHERE userid = @p0 AND programcohortid = @p1", 
                new object[] {UserID, ProgramCohortID}); 

            foreach(CoreTopic topic in ProgramCoreTopics) 
            {
                IEnumerable<CoreTopicScore> topicScores = userScores.Where(s=> s.CoreTopicID == topic.CoreTopicID); 
                SpreadsheetRow row = new SpreadsheetRow() { Scores = new List<CoreTopicScore>(), CoreTopic = topic.CoreTopicNum + " " +
                topic.CoreTopicDesc};

                foreach(Task task in model.CohortTasks)
                {
                    try
                    {
                        CoreTopicScore score = topicScores.First(s => s.TaskID == task.TaskID);
                        row.Scores.Add(score);
                    }
                    catch
                    {
                        row.Scores.Add(new CoreTopicScore());
                    }                                             
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


        public int getStudentCohortID(int UserID)
        {
            return db.CohortEnrollments.FirstOrDefault(e => e.UserID == UserID).ProgramCohortID; 
        }
    }
}