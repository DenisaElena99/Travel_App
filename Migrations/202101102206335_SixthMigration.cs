namespace Travel_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentArticles", "Comment_CommentId", "dbo.Comments");
            DropForeignKey("dbo.CommentArticles", "Article_ArticleId", "dbo.Articles");
            DropIndex("dbo.CommentArticles", new[] { "Comment_CommentId" });
            DropIndex("dbo.CommentArticles", new[] { "Article_ArticleId" });
            AddColumn("dbo.Comments", "ArticleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
            DropColumn("dbo.Articles", "CommentId");
            DropTable("dbo.CommentArticles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CommentArticles",
                c => new
                    {
                        Comment_CommentId = c.Int(nullable: false),
                        Article_ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Comment_CommentId, t.Article_ArticleId });
            
            AddColumn("dbo.Articles", "CommentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropColumn("dbo.Comments", "ArticleId");
            CreateIndex("dbo.CommentArticles", "Article_ArticleId");
            CreateIndex("dbo.CommentArticles", "Comment_CommentId");
            AddForeignKey("dbo.CommentArticles", "Article_ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
            AddForeignKey("dbo.CommentArticles", "Comment_CommentId", "dbo.Comments", "CommentId", cascadeDelete: true);
        }
    }
}
