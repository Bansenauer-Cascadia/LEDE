namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twodockeys : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskVersion", "FeedbackDocID", "dbo.Document");
            DropIndex("dbo.TaskVersion", new[] { "FeedbackDocID" });
            DropIndex("dbo.TaskVersion", new[] { "DocumentID" });
            AlterColumn("dbo.TaskVersion", "FeedbackDocID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.TaskVersion", name: "FeedbackDocID", newName: "DocumentID");
            AddColumn("dbo.TaskVersion", "FeedbackDocID", c => c.Int());
            CreateIndex("dbo.TaskVersion", "DocumentID");
            AddForeignKey("dbo.TaskVersion", "DocumentID", "dbo.Document", "DocumentID", cascadeDelete: true);
        }
    }
}
