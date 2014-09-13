namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ugh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskVersion", "Document_DocumentID", "dbo.Document");
            DropIndex("dbo.TaskVersion", new[] { "Document_DocumentID" });
            DropColumn("dbo.TaskVersion", "Document_DocumentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskVersion", "Document_DocumentID", c => c.Int());
            CreateIndex("dbo.TaskVersion", "Document_DocumentID");
            AddForeignKey("dbo.TaskVersion", "Document_DocumentID", "dbo.Document", "DocumentID");
        }
    }
}
