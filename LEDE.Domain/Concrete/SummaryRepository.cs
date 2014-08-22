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
            IEnumerable<User> cohortStudents = db.Users.Where(u => u.CohortEnrollments.FirstOrDefault(e => e.ProgramCohortID == cohortID) != null);
            List<StudentTotal> cohortTotals = new List<StudentTotal>();

            foreach (User student in cohortStudents)
            {
                StudentTotal studentTotals = new StudentTotal()
                {
                    User = new User(),
                    CTotal = 2,
                    STotal = 3, 
                    PTotal = 4
                };
                cohortTotals.Add(studentTotals);
            }

            return new SeminarSummary(); 
        }

        public StudentSummary getStudentTotals(int Id)
        {
            return new StudentSummary(); 
        }

        public IEnumerable<ProgramCohort> getCohorts()
        {
            return db.ProgramCohorts.Include(c => c.Program); 
        }
    }
}