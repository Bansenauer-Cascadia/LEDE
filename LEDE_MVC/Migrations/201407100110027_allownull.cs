namespace LEDE_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allownull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CoreRating", "Cscore", c => c.Int());
            AlterColumn("dbo.CoreRating", "Pscore", c => c.Int());
            AlterColumn("dbo.CoreRating", "Sscore", c => c.Int());
            AlterColumn("dbo.ImpactTypeRating", "Sscore", c => c.Int());
            AlterColumn("dbo.ImpactTypeRating", "Pscore", c => c.Int());
            AlterColumn("dbo.ImpactTypeRating", "Lscore", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImpactTypeRating", "Lscore", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpactTypeRating", "Pscore", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpactTypeRating", "Sscore", c => c.Int(nullable: false));
            AlterColumn("dbo.CoreRating", "Sscore", c => c.Int(nullable: false));
            AlterColumn("dbo.CoreRating", "Pscore", c => c.Int(nullable: false));
            AlterColumn("dbo.CoreRating", "Cscore", c => c.Int(nullable: false));
        }
    }
}
