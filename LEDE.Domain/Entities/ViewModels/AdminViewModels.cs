using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using LEDE.Domain.Entities;
using System.Web.Mvc;

namespace LEDE.Domain.Entities
{
    public class AdminModel
    {
        public CreateModel Create { get; set; }

        public EditModel Edit { get; set; }

        public IEnumerable<IndexModel> Index { get; set; }
    }
    public class CreateModel
    {
        [Required]
        public string FirstName { get; set; }        

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName {get; set;}

        [Required]
        public string Password { get; set; }

        [Required]
        public string UniversityID{get;set;}

        public IEnumerable<SelectListItem> Roles { get; set; }

        public string SelectedRole { get; set; }
    }

    public class IndexModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }
    }

    public class EditModel
    {
        public User User { get; set; }

        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public Role Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }
}