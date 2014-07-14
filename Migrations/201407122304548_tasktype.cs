namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasktype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskType",
                c => new
                    {
                        TaskTypeID = c.Int(nullable: false, identity: true),
                        TaskTypeDescription = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.TaskTypeID);
            
            AddColumn("dbo.Task", "TaskTypeID", c => c.Int(nullable: true));
            CreateIndex("dbo.Task", "TaskTypeID");
            AddForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType", "TaskTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType");
            DropIndex("dbo.Task", new[] { "TaskTypeID" });
            DropColumn("dbo.Task", "TaskTypeID");
            DropTable("dbo.TaskType");
        }
    }
}
