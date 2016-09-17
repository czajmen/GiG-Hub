namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class followingUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowingArtists",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowingArtists", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FollowingArtists", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.FollowingArtists", new[] { "FolloweeId" });
            DropIndex("dbo.FollowingArtists", new[] { "FollowerId" });
            DropTable("dbo.FollowingArtists");
        }
    }
}
