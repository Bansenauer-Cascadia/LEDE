namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackdocid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskVersion", "FeedbackDoc_DocumentID", "dbo.Document");
            DropIndex("dbo.TaskVersion", new[] { "FeedbackDoc_DocumentID" });
            DropColumn("dbo.TaskVersion", "FeedbackDoc_DocumentID");
        }
        
        public override void Down()
        {
            
        }
    }
}
