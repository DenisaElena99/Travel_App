namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            CreateTable(
                "dbo.CommentArticles",
                c => new
                    {
                        Comment_CommentId = c.Int(nullable: false),
                        Article_ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_CommentId, t.Article_ArticleId })
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_ArticleId, cascadeDelete: true)
                .Index(t => t.Comment_CommentId)
                .Index(t => t.Article_ArticleId);
            
            DropColumn("dbo.Comments", "ArticleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ArticleId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CommentArticles", "Article_ArticleId", "dbo.Articles");
            DropForeignKey("dbo.CommentArticles", "Comment_CommentId", "dbo.Comments");
            DropIndex("dbo.CommentArticles", new[] { "Article_ArticleId" });
            DropIndex("dbo.CommentArticles", new[] { "Comment_CommentId" });
            DropTable("dbo.CommentArticles");
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
        }
    }
}
