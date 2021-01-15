namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Date", c => c.DateTime(nullable: false));
        }
    }
}
