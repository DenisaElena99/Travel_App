namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Destinations", name: "DestinationId", newName: "ArticleId");
            RenameIndex(table: "dbo.Destinations", name: "IX_DestinationId", newName: "IX_ArticleId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Destinations", name: "IX_ArticleId", newName: "IX_DestinationId");
            RenameColumn(table: "dbo.Destinations", name: "ArticleId", newName: "DestinationId");
        }
    }
}
