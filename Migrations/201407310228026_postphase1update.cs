namespace ECSEL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postphase1update : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            AddColumn("dbo.Seminar", "ProgramCohortID", c => c.Int(nullable: false));
            AddColumn("dbo.ProgramCohort", "ProgramCohortDesc", c => c.String(nullable: false, maxLength: 150, unicode: false));
            DropForeignKey("dbo.Task", "Seminar1_SeminarID", "dbo.Seminar");
            DropForeignKey("dbo.Seminar", "ProgramID", "dbo.Program");
            DropIndex("dbo.Task", new[] { "Seminar1_SeminarID" });
            DropIndex("dbo.Seminar", new[] { "ProgramID" });
            DropColumn("dbo.Task", "Seminar1_SeminarID");
            DropColumn("dbo.Seminar", "ProgramID");
            DropColumn("dbo.Program", "ProgramType");
            DropColumn("dbo.ProgramCohort", "Status");
            DropColumn("dbo.ProgramCohort", "AcademicYear");
            CreateIndex("dbo.Seminar", "ProgramCohortID");
            AddForeignKey("dbo.Task", "SeminarID", "dbo.Seminar", "SeminarID");
            AddForeignKey("dbo.Seminar", "ProgramCohortID", "dbo.ProgramCohort", "ProgramCohortID");
        }
    }
}
