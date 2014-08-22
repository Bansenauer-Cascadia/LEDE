using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;

namespace LEDE.Domain.Entities
{
    public class EnrollmentViewModel
    {
        public IEnumerable<ProgramCohort> Cohorts { get; set; }

        public int ProgramCohortID { get; set; }

        public AllUsers Users { get; set; }

        public List<int> getIdsToAdd()
        {
            return getSelectedIds(Users.NonEnrolledFaculty, Users.NonEnrolledCandidates); 
        }

        public List<int> getIdsToRemove()
        {
            return getSelectedIds(Users.EnrolledFaculty, Users.EnrolledCandidates);
        }

        private List<int> getSelectedIds(List<SelectableUser> faculty, List<SelectableUser> candidates)
        {
            List<int> selectedIds = new List<int>(); 
            faculty.AddRange(candidates); 

            foreach (SelectableUser user in faculty)
            {
                if (user.Selected == true)
                    selectedIds.Add(user.Id); 
            }

            return selectedIds; 
        }
    }

    public class SelectableUser
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RoleEnum Role { get; set; }

        public bool Selected { get; set; }
    }

    public class AllUsers
    {
        public List<SelectableUser> EnrolledFaculty { get; set; }

        public List<SelectableUser> EnrolledCandidates { get; set; }

        public List<SelectableUser> NonEnrolledFaculty { get; set; }

        public List<SelectableUser> NonEnrolledCandidates { get; set; }

        public AllUsers()
        {
            EnrolledFaculty = new List<SelectableUser>();
            EnrolledCandidates = new List<SelectableUser>();
            NonEnrolledCandidates = new List<SelectableUser>(); 
            NonEnrolledFaculty = new List<SelectableUser>(); 
        }
    }

    public enum RoleEnum
    {
        Candidate,
        Faculty,
        Admin
    }

    public enum CohortFunction
    {
        Add,
        Remove
    }

}