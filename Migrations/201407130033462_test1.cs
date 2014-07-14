namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType");
            AddForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType", "TaskTypeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType");
            AddForeignKey("dbo.Task", "TaskTypeID", "dbo.TaskType", "TaskTypeID", cascadeDelete: true);
        }
    }
}
