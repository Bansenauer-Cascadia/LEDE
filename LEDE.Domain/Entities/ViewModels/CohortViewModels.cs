using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEDE.Domain.Entities
{
    public class CohortUsers
    {
        public int ProgramCohortID { get; set; }

        public List<SelectableUser> EnrolledFaculty { get; set; }

        public List<SelectableUser> EnrolledCandidates { get; set; }

        public List<SelectableUser> NonEnrolledFaculty { get; set; }

        public List<SelectableUser> NonEnrolledCandidates { get; set; }

        public CohortUsers()
        {
            EnrolledFaculty = new List<SelectableUser>();
            EnrolledCandidates = new List<SelectableUser>();
            NonEnrolledCandidates = new List<SelectableUser>(); 
            NonEnrolledFaculty = new List<SelectableUser>(); 
        }

        public List<int> getIdsToAdd()
        {
            return getSelectedIds(NonEnrolledFaculty, NonEnrolledCandidates);
        }

        public List<int> getIdsToRemove()
        {
            return getSelectedIds(EnrolledFaculty, EnrolledCandidates);
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
}