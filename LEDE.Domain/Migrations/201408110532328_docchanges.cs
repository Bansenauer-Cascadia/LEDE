namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "Blob", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Document", "Container", c => c.String(nullable: false));
            DropColumn("dbo.Document", "FileName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Document", "FileName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Document", "Container");
            DropColumn("dbo.Document", "Blob");
        }
    }
}
