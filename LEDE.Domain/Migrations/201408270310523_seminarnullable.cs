namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seminarnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "SeminarID", "dbo.Seminar");
            DropIndex("dbo.ProgramCohort", new[] { "ProgramID" });
            DropIndex("dbo.Seminar", new[] { "ProgramID" });
            AlterColumn("dbo.ProgramCohort", "ProgramID", c => c.Int());
            AlterColumn("dbo.Seminar", "ProgramID", c => c.Int());
            CreateIndex("dbo.ProgramCohort", "ProgramID");
            CreateIndex("dbo.Seminar", "ProgramID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seminar", new[] { "ProgramID" });
            DropIndex("dbo.ProgramCohort", new[] { "ProgramID" });
            AlterColumn("dbo.Seminar", "ProgramID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProgramCohort", "ProgramID", c => c.Int(nullable: false));
            CreateIndex("dbo.Seminar", "ProgramID");
            CreateIndex("dbo.ProgramCohort", "ProgramID");
            AddForeignKey("dbo.Task", "SeminarID", "dbo.Seminar", "SeminarID");
        }
    }
}
