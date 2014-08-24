using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Concrete
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private DbContext db; 

        public EnrollmentRepository()
        {
            db = new DbContext(); 
        }

        public IEnumerable<ProgramCohort> getCohorts()
        {
            return db.ProgramCohorts; 
        }

        private IEnumerable<SelectableUser> getEnrolledUsers(int ProgramCohortID)
        {
            int FacultyRoleID = db.Roles.Single(r => r.Name == "Faculty").Id;
            int CandidateRoleID = db.Roles.Single(r => r.Name == "Candidate").Id; 

            return db.CohortEnrollments.Where(e => e.ProgramCohortID == ProgramCohortID && e.User.Roles.FirstOrDefault(r=> r.RoleId == FacultyRoleID || r.RoleId == CandidateRoleID) != null).
                Select(e => new SelectableUser() {Id = e.UserID, FirstName = e.User.FirstName, LastName = e.User.LastName, 
                    Role = e.User.Roles.FirstOrDefault(r=> r.RoleId == CandidateRoleID) == null ? RoleEnum.Faculty : RoleEnum.Candidate});
        }


        public CohortUsers getCohortUsers(int ProgramCohortID)
        {
            CohortUsers users = new CohortUsers();
            users.ProgramCohortID = ProgramCohortID;

            int FacultyRoleID = db.Roles.Single(r => r.Name == "Faculty").Id;
            int CandidateRoleID = db.Roles.Single(r => r.Name == "Candidate").Id;

            foreach (User user in db.Users.Where(u => u.Roles.FirstOrDefault(r => r.RoleId == CandidateRoleID || r.RoleId == FacultyRoleID) != null).OrderBy(u=> u.LastName))
            {
                SelectableUser selectableuser = new SelectableUser()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Selected = false,
                    Role = user.Roles.FirstOrDefault(r => r.RoleId == CandidateRoleID) != null ? RoleEnum.Candidate : RoleEnum.Faculty
                };
                if (user.CohortEnrollments.FirstOrDefault(e => e.ProgramCohortID == ProgramCohortID) != null)
                {
                    if (selectableuser.Role == RoleEnum.Candidate)
                        users.EnrolledCandidates.Add(selectableuser);
                    else
                        users.EnrolledFaculty.Add(selectableuser); 
                }
                else
                {
                    if (selectableuser.Role == RoleEnum.Candidate)
                        users.NonEnrolledCandidates.Add(selectableuser);
                    else
                        users.NonEnrolledFaculty.Add(selectableuser); 
                }
            }
            return users; 
        }


        public void addCohortUsers(List<int> idsToAdd, int programCohortID)
        {
            foreach (int id in idsToAdd)
            {
                db.CohortEnrollments.Add(new CohortEnrollment { ProgramCohortID = programCohortID, UserID = id }); 
            }
            db.SaveChanges();
        }

        public void removeCohortUsers(List<int> idsToRemove, int programCohortID)
        {
            foreach (int id in idsToRemove)
            {
                CohortEnrollment removedEnrollment = db.CohortEnrollments.First(e => e.UserID == id && e.ProgramCohortID == programCohortID);
                db.CohortEnrollments.Remove(removedEnrollment); 
            }
            db.SaveChanges(); 
        }

    }
}