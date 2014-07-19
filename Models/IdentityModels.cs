using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ECSEL.Models;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ECSEL.Models
{
    public class UserRole : IdentityUserRole<int> { }
    public class UserClaim : IdentityUserClaim<int> { }
    public class UserLogin : IdentityUserLogin<int> { }

    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<User, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    } 

    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.    

    public class ApplicationDbContext : IdentityDbContext<User, Role,
    int, UserLogin, UserRole, UserClaim> 
    {
        public ApplicationDbContext()
            : base("LEDEdb")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }        
        
        public virtual DbSet<CohortEnrollment> CohortEnrollments { get; set; }
        public virtual DbSet<CoreRating> CoreRatings { get; set; }
        public virtual DbSet<CoreTopic> CoreTopics { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<ImpactTypeRating> ImpactTypeRatings { get; set; }
        public virtual DbSet<InternReflection> InternReflections { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramCohort> ProgramCohorts { get; set; }
        public virtual DbSet<ReadingLog> ReadingLogs { get; set; }        
        public virtual DbSet<Seminar> Seminars { get; set; }
        public virtual DbSet<SubjectiveType> SubjectiveTypes { get; set; }
        public virtual DbSet<SummaryCoreRating> SummaryCoreRatings { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskRating> TaskRatings { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<TaskVersion> TaskVersions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<CohortEnrollment>()
                .Property(e => e.AcademicYear)
                .IsUnicode(false);

            modelBuilder.Entity<CohortEnrollment>()
                .Property(e => e.Quarter)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CohortEnrollment>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CoreTopic>()
                .Property(e => e.CoreTopicNum)
                .HasPrecision(2, 1);

            modelBuilder.Entity<CoreTopic>()
                .Property(e => e.CoreTopicDesc)
                .IsUnicode(false);

            modelBuilder.Entity<CoreTopic>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CoreTopic>()
                .HasMany(e => e.CoreRatings)
                .WithRequired(e => e.CoreTopic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.FilePath)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.FileSize)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.TaskVersions)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.TaskVersions1)
                .WithOptional(e => e.FeedbackDocument)
                .HasForeignKey(e => e.FeedbackDocID);

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.ProgramCohorts)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramCohort>()
                .Property(e => e.ProgramCohortDesc)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramCohort>()
                .HasMany(e => e.CohortEnrollments)
                .WithRequired(e => e.ProgramCohort)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramCohort>()
                .HasMany(e => e.Seminars)
                .WithRequired(e => e.ProgramCohort)
                .WillCascadeOnDelete(false);            

            modelBuilder.Entity<Seminar>()
                .Property(e => e.SeminarTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Seminar>()
                .HasMany(e => e.CoreTopics)
                .WithRequired(e => e.Seminar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seminar>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Seminar)
                .HasForeignKey(e => e.SeminarID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seminar>()
                .HasMany(e => e.Tasks1)
                .WithRequired(e => e.Seminar1)
                .HasForeignKey(e => e.SeminarID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubjectiveType>()
                .Property(e => e.SubjectiveTypeDesc)
                .IsUnicode(false);

            modelBuilder.Entity<SubjectiveType>()
                .Property(e => e.SubjectiveCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SubjectiveType>()
                .HasMany(e => e.SummaryCoreRatings)
                .WithRequired(e => e.SubjectiveType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SummaryCoreRating>()
                .Property(e => e.SubjectiveRating)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.TaskCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.TaskVersions)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaskRating>()
                .HasOptional(e => e.CoreRating)
                .WithRequired(e => e.TaskRating)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TaskRating>()
                .HasOptional(e => e.ImpactTypeRating)
                .WithRequired(e => e.TaskRating);

            modelBuilder.Entity<TaskRating>()
                .HasOptional(e => e.ImpactTypeRating1)
                .WithRequired(e => e.TaskRating1)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TaskVersion>()
                .Property(e => e.RatingStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaskVersion>()
                .HasOptional(e => e.InternReflection)
                .WithRequired(e => e.TaskVersion);

            modelBuilder.Entity<TaskVersion>()
                .HasOptional(e => e.ReadingLogEntry)
                .WithRequired(e => e.TaskVersion);

            modelBuilder.Entity<TaskVersion>()
                .HasMany(e => e.TaskRatings)
                .WithRequired(e => e.TaskVersion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UniversityID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CohortEnrollments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SummaryCoreRatings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CandidateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TaskRatings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.FacultyID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TaskVersions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
    
}

#region Helpers
namespace ECSEL
{
    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public static void SignIn(ApplicationUserManager manager, User user, bool isPersistent)
        {
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[ProviderNameKey];
        }

        public const string CodeKey = "code";
        public static string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";
        public static string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public static string GetResetPasswordRedirectUrl(string code)
        {
            return "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
        }

        public static string GetUserConfirmationRedirectUrl(string code, string userId)
        {
            return "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }
    }
}
#endregion
