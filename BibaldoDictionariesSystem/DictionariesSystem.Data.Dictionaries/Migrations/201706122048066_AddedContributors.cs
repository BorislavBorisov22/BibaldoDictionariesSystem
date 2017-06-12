namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContributors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        GithubProfile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contributors");
        }
    }
}
