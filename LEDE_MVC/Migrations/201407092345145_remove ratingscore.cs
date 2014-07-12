namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeratingscore : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaskRating", "RatingScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskRating", "RatingScore", c => c.Int(nullable: false));
        }
    }
}
