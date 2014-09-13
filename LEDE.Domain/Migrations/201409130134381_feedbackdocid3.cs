namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackdocid3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaskVersion", "FeedbackDocID");
            RenameColumn(table: "dbo.TaskVersion", name: "FeedbackDoc_DocumentID", newName: "FeedbackDocID");
            RenameIndex(table: "dbo.TaskVersion", name: "IX_FeedbackDoc_DocumentID", newName: "IX_FeedbackDocID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TaskVersion", name: "IX_FeedbackDocID", newName: "IX_FeedbackDoc_DocumentID");
            RenameColumn(table: "dbo.TaskVersion", name: "FeedbackDocID", newName: "FeedbackDoc_DocumentID");
            AddColumn("dbo.TaskVersion", "FeedbackDocID", c => c.Int());
        }
    }
}
