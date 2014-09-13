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
            IEnumerable<User> cohortStudents = db.Users.Where(u => u.CohortEnrollments.Any(e => e.ProgramCohortID == cohortID) &&
                u.Roles.Any(r => r.RoleId == 1));
            int ProgramID = db.ProgramCohorts.Find(cohortID).ProgramID;

            var TaskTotals = from cr in db.CoreRatings
                             where cr.TaskRating.TaskVersion.Task.Seminar.ProgramID == ProgramID && cr.TaskRating.TaskVersion.UserID == userID
                             group cr by new { cr.TaskRating.TaskVersion.TaskID, cr.CoreTopic } into g
                             let maxVersion = g.Max(cr => cr.TaskRating.TaskVersion.Version)
                             select new
                             {
                                 CScore = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).Cscore,
                                 SScore = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).Sscore,
                                 PScore = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).Pscore,
                                 TaskID = g.Key.TaskID,
                                 CoreTopic = g.Key.CoreTopic
                             };

            var UserTotals = from tt in TaskTotals
                             group tt by tt.CoreTopic into g
                             select new CoreTotal()
                             {
                                 CTotal = g.Sum(tt => tt.CScore) ?? 0,
                                 STotal = g.Sum(tt => tt.SScore) ?? 0,
                                 PTotal = g.Sum(tt => tt.PScore) ?? 0,
                                 OneCount = g.Count(tt => tt.CScore == 1) + g.Count(tt => tt.SScore == 1) + g.Count(tt => tt.PScore == 1),
                                 TwoCount = g.Count(tt => tt.CScore == 2) + g.Count(tt => tt.SScore == 2) + g.Count(tt => tt.PScore == 2),
                                 ThreeCount = g.Count(tt => tt.CScore == 3) + g.Count(tt => tt.SScore == 3) + g.Count(tt => tt.PScore == 3),
                                 CoreTopic = g.Key
                             };

            var CombinedTotals = UserTotals.Select(u => new { Total = u.CTotal + u.STotal + u.PTotal });
            var CombinedCounts = UserTotals.Select(u => new { Count = u.OneCount + u.TwoCount + u.ThreeCount });
            StudentSummary model = new StudentSummary()
            {
                RatingsList = UserTotals.ToList(),
                User = db.Users.Find(userID),
                MaxTotal = CombinedTotals.Any() ? CombinedTotals.Max(t => t.Total) : 0,
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
                "SELECT * FROM coretopicscores WHERE userid = 1 AND programcohortid = 1"); 

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


        public SummaryModel getSummaryCohorts(int UserID)
        {
            var userCohorts = db.CohortEnrollments.Where(e => e.UserID == UserID).Select(e =>
                    new SelectListItem(){Value = e.ProgramCohortID.ToString(), Text = e.ProgramCohort.Program.ProgramTitle 
                        + " " + e.ProgramCohort.AcademicYear });            
            SummaryModel model = new SummaryModel()
            {
                ProgramCohorts = userCohorts,
                ProgramCohortID = Convert.ToInt32(userCohorts.First().Value) 
            };

            return model; 
        }

        public void getSummaryCandidates(SummaryModel model)
        {
            var candidates = db.CohortEnrollments.Where(e => e.ProgramCohortID == model.ProgramCohortID).Select(e =>
                new SelectListItem() { Value = e.UserID.ToString(), Text = e.User.LastName + ", " +e.User.FirstName}).OrderBy(c=> c.Text);
            model.Candidates = candidates;
        }
    }
}