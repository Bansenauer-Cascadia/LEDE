namespace ECSEL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shouldbenull : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Seminar", "ProgramCohortID", "dbo.ProgramCohort");
            DropForeignKey("dbo.Task", "SeminarID", "dbo.Seminar");
            DropForeignKey("dbo.CoreTopic", "SeminarID", "dbo.Seminar");
            DropForeignKey("dbo.CoreRating", "CoreTopicID", "dbo.CoreTopic");
            DropForeignKey("dbo.TaskVersion", "UserID", "dbo.Users");
            DropForeignKey("dbo.TaskRating", "FacultyID", "dbo.Users");
            DropForeignKey("dbo.SummaryCoreRating", "CandidateID", "dbo.Users");
            DropForeignKey("dbo.SummaryCoreRating", "User1_Id1", "dbo.Users");
            DropForeignKey("dbo.SummaryCoreRating", "SubjectiveTypeID", "dbo.SubjectiveType");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.CohortEnrollment", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.TaskRating", "VersID", "dbo.TaskVersion");
            DropForeignKey("dbo.TaskVersion", "TaskID", "dbo.Task");
            DropForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType");
            DropForeignKey("dbo.ReadingLogEntry", "VersID", "dbo.TaskVersion");
            DropForeignKey("dbo.InternReflection", "VersID", "dbo.TaskVersion");
            DropForeignKey("dbo.TaskVersion", "FeedbackDocID", "dbo.Document");
            DropForeignKey("dbo.TaskVersion", "DocumentID", "dbo.Document");
            DropForeignKey("dbo.ImpactTypeRating", "RatingID", "dbo.TaskRating");
            DropForeignKey("dbo.CoreRating", "RatingID", "dbo.TaskRating");
            DropForeignKey("dbo.ProgramCohort", "ProgramID", "dbo.Program");
            DropForeignKey("dbo.CohortEnrollment", "ProgramCohortID", "dbo.ProgramCohort");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.SummaryCoreRating", new[] { "User1_Id1" });
            DropIndex("dbo.SummaryCoreRating", new[] { "SubjectiveTypeID" });
            DropIndex("dbo.SummaryCoreRating", new[] { "CandidateID" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Task", new[] { "TaskTypeID" });
            DropIndex("dbo.Task", new[] { "SeminarID" });
            DropIndex("dbo.ReadingLogEntry", new[] { "VersID" });
            DropIndex("dbo.InternReflection", new[] { "VersID" });
            DropIndex("dbo.TaskVersion", new[] { "FeedbackDocID" });
            DropIndex("dbo.TaskVersion", new[] { "UserID" });
            DropIndex("dbo.TaskVersion", new[] { "DocumentID" });
            DropIndex("dbo.TaskVersion", new[] { "TaskID" });
            DropIndex("dbo.ImpactTypeRating", new[] { "RatingID" });
            DropIndex("dbo.TaskRating", new[] { "FacultyID" });
            DropIndex("dbo.TaskRating", new[] { "VersID" });
            DropIndex("dbo.CoreRating", new[] { "CoreTopicID" });
            DropIndex("dbo.CoreRating", new[] { "RatingID" });
            DropIndex("dbo.CoreTopic", new[] { "SeminarID" });
            DropIndex("dbo.Seminar", new[] { "ProgramCohortID" });
            DropIndex("dbo.ProgramCohort", new[] { "ProgramID" });
            DropIndex("dbo.CohortEnrollment", new[] { "UserID" });
            DropIndex("dbo.CohortEnrollment", new[] { "ProgramCohortID" });
            DropTable("dbo.Roles");
            DropTable("dbo.SubjectiveType");
            DropTable("dbo.SummaryCoreRating");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.TaskType");
            DropTable("dbo.Task");
            DropTable("dbo.ReadingLogEntry");
            DropTable("dbo.InternReflection");
            DropTable("dbo.Document");
            DropTable("dbo.TaskVersion");
            DropTable("dbo.ImpactTypeRating");
            DropTable("dbo.TaskRating");
            DropTable("dbo.CoreRating");
            DropTable("dbo.CoreTopic");
            DropTable("dbo.Seminar");
            DropTable("dbo.Program");
            DropTable("dbo.ProgramCohort");
            DropTable("dbo.CohortEnrollment");
        }
    }
}
