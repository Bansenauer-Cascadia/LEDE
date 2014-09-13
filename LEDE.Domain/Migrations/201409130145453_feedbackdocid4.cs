namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackdocid4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskVersion", "Document_DocumentID1", "dbo.Document");
            DropIndex("dbo.TaskVersion", new[] { "Document_DocumentID1" });
            DropColumn("dbo.TaskVersion", "DocumentID");
            RenameColumn(table: "dbo.TaskVersion", name: "Document_DocumentID1", newName: "DocumentID");
            AlterColumn("dbo.TaskVersion", "DocumentID", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskVersion", "DocumentID");
            AddForeignKey("dbo.TaskVersion", "DocumentID", "dbo.Document", "DocumentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskVersion", "DocumentID", "dbo.Document");
            DropIndex("dbo.TaskVersion", new[] { "DocumentID" });
            AlterColumn("dbo.TaskVersion", "DocumentID", c => c.Int());
            RenameColumn(table: "dbo.TaskVersion", name: "DocumentID", newName: "Document_DocumentID1");
            AddColumn("dbo.TaskVersion", "DocumentID", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskVersion", "Document_DocumentID1");
            AddForeignKey("dbo.TaskVersion", "Document_DocumentID1", "dbo.Document", "DocumentID");
        }
    }
}
