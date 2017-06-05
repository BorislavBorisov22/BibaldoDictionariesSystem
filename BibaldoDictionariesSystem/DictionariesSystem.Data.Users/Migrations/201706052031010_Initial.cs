namespace DictionariesSystem.Data.Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RequiredContributions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Passhash = c.String(),
                        ContributionsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Badge_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Badge_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Badges", t => t.Badge_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Badge_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBadges", "Badge_Id", "dbo.Badges");
            DropForeignKey("dbo.UserBadges", "User_Id", "dbo.Users");
            DropIndex("dbo.UserBadges", new[] { "Badge_Id" });
            DropIndex("dbo.UserBadges", new[] { "User_Id" });
            DropTable("dbo.UserBadges");
            DropTable("dbo.Users");
            DropTable("dbo.Badges");
        }
    }
}
