namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cohortcleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "ProgramCohort_ProgramCohortID", "dbo.ProgramCohort");
            DropIndex("dbo.Task", new[] { "ProgramCohort_ProgramCohortID" });           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "ProgramCohort_ProgramCohortID", c => c.Int());
            CreateIndex("dbo.Task", "ProgramCohort_ProgramCohortID");
            AddForeignKey("dbo.Task", "ProgramCohort_ProgramCohortID", "dbo.ProgramCohort", "ProgramCohortID");
        }
    }
}
