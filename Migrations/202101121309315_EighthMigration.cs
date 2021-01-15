namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EighthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Articles", "UserId");
            AddForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropColumn("dbo.Articles", "UserId");
        }
    }
}
