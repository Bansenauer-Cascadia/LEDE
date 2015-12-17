namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backtonormal : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskVersion", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
