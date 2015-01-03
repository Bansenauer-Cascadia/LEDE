using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Concrete
{
    public class FacultyAccess
    {
        private DbContext db;

        public FacultyAccess()
        {
            this.db = new DbContext();
        }

        public void Validate(int FacultyID, int VersID)
        {
            try {
                TaskVersion CandidateTaskVersion = db.TaskVersions.Find(VersID);
                int ProgramID = CandidateTaskVersion.Task.Seminar.ProgramID;
                int CandidateID = CandidateTaskVersion.UserID;
                int ProgramCohortID = db.CohortEnrollments.First(ce => ce.ProgramCohort.ProgramID == ProgramID 
                    && ce.UserID == CandidateID).ProgramCohortID;

                db.CohortEnrollments.First(ce => ce.UserID == FacultyID && ce.ProgramCohortID == ProgramCohortID);
            }
            catch {
                throw new Exception("Faculty does not have access to taskversion identified by Given VersID");
            }
        }
    }
}