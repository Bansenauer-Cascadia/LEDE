namespace LEDE.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class document2 : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Document", "FilePath", c => c.String(nullable: false, unicode: false));
            DropColumn("dbo.Document", "FileName");
        }
    }
}
