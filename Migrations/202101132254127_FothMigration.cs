namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FothMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "UserId");
        }
    }
}
