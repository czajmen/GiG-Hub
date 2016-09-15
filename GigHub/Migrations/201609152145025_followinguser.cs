namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class followinguser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowingArtists",
                c => new
                    {
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        FollowerId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ArtistId, t.FollowerId })
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowingArtists", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowingArtists", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowingArtists", new[] { "FollowerId" });
            DropIndex("dbo.FollowingArtists", new[] { "ArtistId" });
            DropTable("dbo.FollowingArtists");
        }
    }
}
