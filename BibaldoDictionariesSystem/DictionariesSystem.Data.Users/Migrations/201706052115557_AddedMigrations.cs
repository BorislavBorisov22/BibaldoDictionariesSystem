namespace DictionariesSystem.Data.Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMigrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Badges", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Passhash", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "ContributionsCount", c => c.Int());
            CreateIndex("dbo.Badges", "Name", unique: true);
            CreateIndex("dbo.Users", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Badges", new[] { "Name" });
            AlterColumn("dbo.Users", "ContributionsCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Passhash", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Badges", "Name", c => c.String());
        }
    }
}
