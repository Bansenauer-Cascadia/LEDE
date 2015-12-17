using System;
using Microsoft.AspNet.Identity.EntityFramework;
using LEDE.Domain.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEDE.Domain.Entities
{

    public class User : IdentityUser <int, UserLogin, UserRole, UserClaim>
    {
        [Required]
        [StringLength(15)]
        public string UniversityID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<CohortEnrollment> CohortEnrollments { get; set; }

        public virtual ICollection<SummaryCoreRating> SummaryCoreRatings { get; set; }

        public virtual ICollection<TaskRating> TaskRatings { get; set; }

        public virtual ICollection<TaskVersion> TaskVersions { get; set; }
    }

    public class Role : IdentityRole <int, UserRole>
    {
        public Role() { }

        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim> 
    {
        public UserStore(DbContext context) : base(context) { } 
    }

    public class RoleStore : RoleStore<Role, int, UserRole> 
    {
        public RoleStore(DbContext context) : base(context) { }
    }

    public class UserRole : IdentityUserRole<int> { }

    public class UserClaim : IdentityUserClaim<int> { }

    public class UserLogin : IdentityUserLogin<int> { }
}

