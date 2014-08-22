using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Concrete
{
    public class DbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public DbContext() : base("lededb") { }

        static DbContext()
        {
            Database.SetInitializer<DbContext>(new IdentityDbInit());
        }
        public static DbContext Create()
        {
            return new DbContext();
        }

        public virtual DbSet<CohortEnrollment> CohortEnrollments { get; set; }
        public virtual DbSet<CoreRating> CoreRatings { get; set; }
        public virtual DbSet<CoreTopic> CoreTopics { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<ImpactTypeRating> ImpactTypeRatings { get; set; }
        public virtual DbSet<InternReflection> InternReflections { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramCohort> ProgramCohorts { get; set; }
        public virtual DbSet<Rater> Raters { get; set; }
        public virtual DbSet<ReadingLogEntry> ReadingLogEntries { get; set; }
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
                .Property(e => e.Blob)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.FileSize)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.TaskVersions)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramType)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.ProgramCohorts)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.Seminars)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramCohort>()
                .Property(e => e.AcademicYear)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramCohort>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramCohort>()
                .HasMany(e => e.CohortEnrollments)
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
                .HasMany(e => e.Raters)
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
        }
    }



    public class IdentityDbInit
            : DropCreateDatabaseIfModelChanges<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(DbContext context)
        {
            // initial configuration will go here
        }
    }
}