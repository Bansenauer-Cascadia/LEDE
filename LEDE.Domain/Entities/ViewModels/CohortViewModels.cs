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

    public class ListViewModel
    {
        public IEnumerable<ListItem> Items { get; set; }

        public string Entity { get; set; }

        public string deleteAction { get; set; }

        public string editAction { get; set; }

        public string createAction { get; set; }
    }

    public class ParentViewModel : ListViewModel
    {        
        public IEnumerable<ChildEntity> ChildEntities { get; set; }

        public int SelectedItemID { get; set; }
    }

    public class ChildViewModel 
    {
        public int ParentID { get; set; }

        public IEnumerable<ListViewModel> Items { get; set; }
    }

    public class ListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ChildEntity
    {
        public string Entity { get; set; }
        public string Action { get; set; }
    }
}

/*
public class GenericEnrollment<T> where T : SelectableItem
    {
        public int Id { get; set; }

        public EnrollmentType Type { get; set; }

        public List<T> enrolledItems { get; set; }

        public List<T> nonEnrolledItems { get; set; }

        public List<int> getIdsToAdd()
        {
            return getSelectedIds(nonEnrolledItems);
        }

        public List<int> getIdsToRemove()
        {
            return getSelectedIds(enrolledItems);
        }

        private List<int> getSelectedIds(List<T> items)
        {
            List<int> selectedIds = new List<int>();

            foreach (T item in items)
            {
                if (item.Selected == true)
                    selectedIds.Add(item.Id);
            }

            return selectedIds;
        }
    }

    public class SelectableItem
    {
        public int Id { get; set; }

        public bool Selected { get; set; }

        public string Name { get; set; }
    }

    public enum EnrollmentType
    {
        ProgramCohort,
        Seminar
    }*/