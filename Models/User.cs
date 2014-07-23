using System;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ECSEL.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace ECSEL.Models
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
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

        public User()
        {
            CohortEnrollments = new HashSet<CohortEnrollment>();
            SummaryCoreRatings = new HashSet<SummaryCoreRating>();
            TaskRatings = new HashSet<TaskRating>();
            TaskVersions = new HashSet<TaskVersion>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this,
                               DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}