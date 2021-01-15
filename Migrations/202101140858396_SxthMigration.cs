namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SxthMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_UserId");
            AddColumn("dbo.Articles", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Articles", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "CategoryId");
            DropColumn("dbo.Articles", "Date");
            RenameIndex(table: "dbo.Comments", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "User_Id");
        }
    }
}
