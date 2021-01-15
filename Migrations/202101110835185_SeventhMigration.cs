namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeventhMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Destinations", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Destinations", "Name", c => c.Int(nullable: false));
        }
    }
}
