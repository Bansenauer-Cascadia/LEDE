namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CohortEnrollment",
                c => new
                    {
                        CohortEventID = c.Int(nullable: false, identity: true),
                        ProgramCohortID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CohortEventID)
                .ForeignKey("dbo.ProgramCohort", t => t.ProgramCohortID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProgramCohortID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ProgramCohort",
                c => new
                    {
                        ProgramCohortID = c.Int(nullable: false, identity: true),
                        ProgramID = c.Int(nullable: false),
                        AcademicYear = c.String(nullable: false, maxLength: 50, unicode: false),
                        Status = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ProgramCohortID)
                .ForeignKey("dbo.Program", t => t.ProgramID)
                .Index(t => t.ProgramID);
            
            CreateTable(
                "dbo.Program",
                c => new
                    {
                        ProgramID = c.Int(nullable: false, identity: true),
                        ProgramTitle = c.String(nullable: false, unicode: false),
                        ProgramType = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ProgramID);
            
            CreateTable(
                "dbo.Seminar",
                c => new
                    {
                        SeminarID = c.Int(nullable: false, identity: true),
                        SeminarTitle = c.String(nullable: false, maxLength: 250, unicode: false),
                        ProgramID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeminarID)
                .ForeignKey("dbo.Program", t => t.ProgramID)
                .Index(t => t.ProgramID);
            
            CreateTable(
                "dbo.CoreTopic",
                c => new
                    {
                        CoreTopicID = c.Int(nullable: false, identity: true),
                        CoreTopicNum = c.Decimal(nullable: false, precision: 2, scale: 1),
                        CoreTopicDesc = c.String(nullable: false, unicode: false),
                        SeminarID = c.Int(nullable: false),
                        ModifyDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.CoreTopicID)
                .ForeignKey("dbo.Seminar", t => t.SeminarID)
                .Index(t => t.SeminarID);
            
            CreateTable(
                "dbo.CoreRating",
                c => new
                    {
                        RatingID = c.Int(nullable: false),
                        CoreTopicID = c.Int(nullable: false),
                        Cscore = c.Int(),
                        Sscore = c.Int(),
                        Pscore = c.Int(),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.TaskRating", t => t.RatingID, cascadeDelete: true)
                .ForeignKey("dbo.CoreTopic", t => t.CoreTopicID)
                .Index(t => t.RatingID)
                .Index(t => t.CoreTopicID);
            
            CreateTable(
                "dbo.TaskRating",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        VersID = c.Int(nullable: false),
                        FacultyID = c.Int(nullable: false),
                        ReviewDate = c.DateTime(nullable: false, storeType: "date"),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.TaskVersion", t => t.VersID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.VersID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ImpactTypeRating",
                c => new
                    {
                        RatingID = c.Int(nullable: false),
                        Sscore = c.Int(),
                        Pscore = c.Int(),
                        Lscore = c.Int(),
                    })
                .PrimaryKey(t => t.RatingID)
                .ForeignKey("dbo.TaskRating", t => t.RatingID)
                .Index(t => t.RatingID);
            
            CreateTable(
                "dbo.TaskVersion",
                c => new
                    {
                        VersID = c.Int(nullable: false, identity: true),
                        TaskID = c.Int(nullable: false),
                        DocumentID = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        RatingStatus = c.String(nullable: false, maxLength: 15, fixedLength: true, unicode: false),
                        UserID = c.Int(nullable: false),
                        FeedbackDocID = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        FeedbackDoc_DocumentID = c.Int(),
                    })
                .PrimaryKey(t => t.VersID)
                .ForeignKey("dbo.Document", t => t.DocumentID)
                .ForeignKey("dbo.Document", t => t.FeedbackDoc_DocumentID)
                .ForeignKey("dbo.Task", t => t.TaskID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.TaskID)
                .Index(t => t.DocumentID)
                .Index(t => t.UserID)
                .Index(t => t.FeedbackDoc_DocumentID);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        DocumentID = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 50, unicode: false),
                        FilePath = c.String(nullable: false, unicode: false),
                        FileSize = c.String(nullable: false, maxLength: 25, unicode: false),
                        UploadDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.DocumentID);
            
            CreateTable(
                "dbo.InternReflection",
                c => new
                    {
                        VersID = c.Int(nullable: false),
                        NumHrs = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VersID)
                .ForeignKey("dbo.TaskVersion", t => t.VersID)
                .Index(t => t.VersID);
            
            CreateTable(
                "dbo.ReadingLogEntry",
                c => new
                    {
                        VersID = c.Int(nullable: false),
                        NumEntries = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VersID)
                .ForeignKey("dbo.TaskVersion", t => t.VersID)
                .Index(t => t.VersID);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskCode = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        TaskName = c.String(nullable: false, maxLength: 100, unicode: false),
                        SeminarID = c.Int(nullable: false),
                        TaskTypeID = c.Int(),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.TaskType", t => t.TaskTypeID)
                .ForeignKey("dbo.Seminar", t => t.SeminarID)
                .Index(t => t.SeminarID)
                .Index(t => t.TaskTypeID);
            
            CreateTable(
                "dbo.TaskType",
                c => new
                    {
                        TaskTypeID = c.Int(nullable: false, identity: true),
                        TaskTypeDescription = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.TaskTypeID);
            
            CreateTable(
                "dbo.Rater",
                c => new
                    {
                        SeminarID = c.Int(nullable: false),
                        FacultyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeminarID, t.FacultyID })
                .ForeignKey("dbo.Seminar", t => t.SeminarID)
                .Index(t => t.SeminarID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SubjectiveType",
                c => new
                    {
                        SubjectiveTypeID = c.Int(nullable: false, identity: true),
                        SubjectiveTypeDesc = c.String(nullable: false, maxLength: 50, unicode: false),
                        SubjectiveCode = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.SubjectiveTypeID);
            
            CreateTable(
                "dbo.SummaryCoreRating",
                c => new
                    {
                        SumRatingID = c.Int(nullable: false, identity: true),
                        CandidateID = c.Int(nullable: false),
                        FacultyID = c.Int(nullable: false),
                        CohortEventID = c.Int(nullable: false),
                        CoreTopicID = c.Int(nullable: false),
                        SubjectiveTypeID = c.Int(nullable: false),
                        SubjectiveRating = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        User1_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SumRatingID)
                .ForeignKey("dbo.SubjectiveType", t => t.SubjectiveTypeID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.SubjectiveTypeID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniversityID = c.String(nullable: false, maxLength: 15),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskVersion", "UserID", "dbo.Users");
            DropForeignKey("dbo.TaskRating", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SummaryCoreRating", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.CohortEnrollment", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.SummaryCoreRating", "SubjectiveTypeID", "dbo.SubjectiveType");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Seminar", "ProgramID", "dbo.Program");
            DropForeignKey("dbo.Task", "SeminarID", "dbo.Seminar");
            DropForeignKey("dbo.Rater", "SeminarID", "dbo.Seminar");
            DropForeignKey("dbo.CoreTopic", "SeminarID", "dbo.Seminar");
            DropForeignKey("dbo.CoreRating", "CoreTopicID", "dbo.CoreTopic");
            DropForeignKey("dbo.TaskRating", "VersID", "dbo.TaskVersion");
            DropForeignKey("dbo.TaskVersion", "TaskID", "dbo.Task");
            DropForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType");
            DropForeignKey("dbo.ReadingLogEntry", "VersID", "dbo.TaskVersion");
            DropForeignKey("dbo.InternReflection", "VersID", "dbo.TaskVersion");
            DropForeignKey("dbo.TaskVersion", "FeedbackDoc_DocumentID", "dbo.Document");
            DropForeignKey("dbo.TaskVersion", "DocumentID", "dbo.Document");
            DropForeignKey("dbo.ImpactTypeRating", "RatingID", "dbo.TaskRating");
            DropForeignKey("dbo.CoreRating", "RatingID", "dbo.TaskRating");
            DropForeignKey("dbo.ProgramCohort", "ProgramID", "dbo.Program");
            DropForeignKey("dbo.CohortEnrollment", "ProgramCohortID", "dbo.ProgramCohort");
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.SummaryCoreRating", new[] { "User_Id" });
            DropIndex("dbo.SummaryCoreRating", new[] { "SubjectiveTypeID" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Rater", new[] { "SeminarID" });
            DropIndex("dbo.Task", new[] { "TaskTypeID" });
            DropIndex("dbo.Task", new[] { "SeminarID" });
            DropIndex("dbo.ReadingLogEntry", new[] { "VersID" });
            DropIndex("dbo.InternReflection", new[] { "VersID" });
            DropIndex("dbo.TaskVersion", new[] { "FeedbackDoc_DocumentID" });
            DropIndex("dbo.TaskVersion", new[] { "UserID" });
            DropIndex("dbo.TaskVersion", new[] { "DocumentID" });
            DropIndex("dbo.TaskVersion", new[] { "TaskID" });
            DropIndex("dbo.ImpactTypeRating", new[] { "RatingID" });
            DropIndex("dbo.TaskRating", new[] { "User_Id" });
            DropIndex("dbo.TaskRating", new[] { "VersID" });
            DropIndex("dbo.CoreRating", new[] { "CoreTopicID" });
            DropIndex("dbo.CoreRating", new[] { "RatingID" });
            DropIndex("dbo.CoreTopic", new[] { "SeminarID" });
            DropIndex("dbo.Seminar", new[] { "ProgramID" });
            DropIndex("dbo.ProgramCohort", new[] { "ProgramID" });
            DropIndex("dbo.CohortEnrollment", new[] { "UserID" });
            DropIndex("dbo.CohortEnrollment", new[] { "ProgramCohortID" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.SummaryCoreRating");
            DropTable("dbo.SubjectiveType");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Rater");
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
