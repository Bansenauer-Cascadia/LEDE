namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hmm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "SeminarID", "dbo.Seminar");
            AddForeignKey("dbo.Task", "SeminarID", "dbo.Seminar", "SeminarID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "SeminarID", "dbo.Seminar");
            AddForeignKey("dbo.Task", "SeminarID", "dbo.Seminar", "SeminarID", cascadeDelete: true);
        }
    }
}
