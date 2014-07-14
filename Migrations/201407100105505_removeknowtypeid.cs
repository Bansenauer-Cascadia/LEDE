namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeknowtypeid : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.CoreRating", "KnowTypeID", c => c.Int(nullable: false));
        }
    }
}
