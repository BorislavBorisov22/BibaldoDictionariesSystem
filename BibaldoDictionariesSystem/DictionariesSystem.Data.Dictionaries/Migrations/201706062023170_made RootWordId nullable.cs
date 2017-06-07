namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeRootWordIdnullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Words", new[] { "RootWordId" });
            AlterColumn("dbo.Words", "RootWordId", c => c.Int());
            CreateIndex("dbo.Words", "RootWordId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Words", new[] { "RootWordId" });
            AlterColumn("dbo.Words", "RootWordId", c => c.Int(nullable: false));
            CreateIndex("dbo.Words", "RootWordId");
        }
    }
}
