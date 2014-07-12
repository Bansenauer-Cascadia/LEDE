namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uhoh : DbMigration
    {
        public override void Up()
        {            
            AddForeignKey("dbo.ImpactTypeRating", "RatingID", "dbo.TaskRating", "RatingID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImpactTypeRating", "RatingID", "dbo.TaskRating");
            
        }
    }
}
