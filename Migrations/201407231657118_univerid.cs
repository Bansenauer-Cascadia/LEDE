namespace ECSEL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class univerid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UniversityID", c => c.String(nullable: false, maxLength: 15, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UniversityID", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
    }
}
