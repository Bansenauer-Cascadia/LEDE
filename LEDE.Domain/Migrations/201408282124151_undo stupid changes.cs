namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undostupidchanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProgramCohort", "ProgramID", "dbo.Program");
            DropForeignKey("dbo.Seminar", "ProgramID", "dbo.Program");
            DropIndex("dbo.ProgramCohort", new[] { "ProgramID" });
            DropIndex("dbo.Seminar", new[] { "ProgramID" });
            AlterColumn("dbo.ProgramCohort", "ProgramID", c => c.Int(nullable: false));
            AlterColumn("dbo.Seminar", "ProgramID", c => c.Int(nullable: false));
            CreateIndex("dbo.ProgramCohort", "ProgramID");
            CreateIndex("dbo.Seminar", "ProgramID");
            AddForeignKey("dbo.ProgramCohort", "ProgramID", "dbo.Program", "ProgramID", cascadeDelete: true);
            AddForeignKey("dbo.Seminar", "ProgramID", "dbo.Program", "ProgramID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seminar", "ProgramID", "dbo.Program");
            DropForeignKey("dbo.ProgramCohort", "ProgramID", "dbo.Program");
            DropIndex("dbo.Seminar", new[] { "ProgramID" });
            DropIndex("dbo.ProgramCohort", new[] { "ProgramID" });
            AlterColumn("dbo.Seminar", "ProgramID", c => c.Int());
            AlterColumn("dbo.ProgramCohort", "ProgramID", c => c.Int());
            CreateIndex("dbo.Seminar", "ProgramID");
            CreateIndex("dbo.ProgramCohort", "ProgramID");
            AddForeignKey("dbo.Seminar", "ProgramID", "dbo.Program", "ProgramID");
            AddForeignKey("dbo.ProgramCohort", "ProgramID", "dbo.Program", "ProgramID");
        }
    }
}
