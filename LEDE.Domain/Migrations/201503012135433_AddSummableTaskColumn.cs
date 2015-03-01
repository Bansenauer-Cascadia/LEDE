namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummableTaskColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "SumScores", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Task", "SumScores");
        }
    }
}
