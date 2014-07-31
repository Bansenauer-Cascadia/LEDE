namespace ECSEL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "Seminar1_SeminarID", c => c.Int());
            CreateIndex("dbo.Task", "Seminar1_SeminarID");
            AddForeignKey("dbo.Task", "Seminar1_SeminarID", "dbo.Seminar", "SeminarID");
        }
    }
}
