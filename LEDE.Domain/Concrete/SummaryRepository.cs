using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; 
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities; 

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

        public SeminarSummary getCohortTotals(int cohortID)
        {
            cohortID = 2; 
            IEnumerable<User> cohortStudents = db.Users.Where(u => u.CohortEnrollments.Any(e => e.ProgramCohortID == cohortID) &&
                u.Roles.Any(r=> r.RoleId == 1));
            int ProgramID = db.ProgramCohorts.SingleOrDefault(c=> c.ProgramCohortID == cohortID).ProgramID;
            
            var TaskTotals = from cr in db.CoreRatings
                             where cr.TaskRating.TaskVersion.Task.Seminar.ProgramID == ProgramID
                             group cr by new { cr.TaskRating.TaskVersion.UserID, cr.TaskRating.TaskVersion.TaskID, cr.CoreTopicID } into g
                             let maxVersion = g.Max(cr => cr.TaskRating.TaskVersion.Version)
                             select new { CScore = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).Cscore, 
                                          SScore = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).Sscore, 
                                          PScore = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).Pscore, 
                                          TaskID = g.Key.TaskID,
                                          CoreTopicID = g.Key.CoreTopicID,
                                          Candidate = g.FirstOrDefault(cr => cr.TaskRating.TaskVersion.Version == maxVersion).TaskRating.TaskVersion.User
                             };
            var UserTotals = from tt in TaskTotals
                             group tt by tt.Candidate.Id into g
                             select new StudentTotal()
                             {
                                 CTotal = g.Sum(tt => tt.CScore) ?? 0,
                                 STotal = g.Sum(tt => tt.SScore) ?? 0,
                                 PTotal = g.Sum(tt => tt.PScore) ?? 0,
                                 User = g.FirstOrDefault().Candidate
                             };
            SeminarSummary model = new SeminarSummary()
            {
                TotalsList = UserTotals,
                MaxTotal = UserTotals.Select(u => new {Total = u.CTotal + u.STotal + u.PTotal }).Max(u=> u.Total)
            };

            return model; 
        }

        public StudentSummary getStudentTotals(int Id)
        {
            return new StudentSummary(); 
        }

        public IEnumerable<ProgramCohort> getCohorts()
        {
            return db.ProgramCohorts; 
        }
    }
}