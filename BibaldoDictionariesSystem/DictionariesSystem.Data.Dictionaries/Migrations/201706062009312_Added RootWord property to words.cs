namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedRootWordpropertytowords : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WordWords", "Word_Id", "dbo.Words");
            DropForeignKey("dbo.WordWords", "Word_Id1", "dbo.Words");
            DropIndex("dbo.WordWords", new[] { "Word_Id" });
            DropIndex("dbo.WordWords", new[] { "Word_Id1" });
            AddColumn("dbo.Words", "RootWordId", c => c.Int(nullable: false));
            CreateIndex("dbo.Words", "RootWordId");
            AddForeignKey("dbo.Words", "RootWordId", "dbo.Words", "Id");
            DropTable("dbo.WordWords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WordWords",
                c => new
                    {
                        Word_Id = c.Int(nullable: false),
                        Word_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Word_Id, t.Word_Id1 });
            
            DropForeignKey("dbo.Words", "RootWordId", "dbo.Words");
            DropIndex("dbo.Words", new[] { "RootWordId" });
            DropColumn("dbo.Words", "RootWordId");
            CreateIndex("dbo.WordWords", "Word_Id1");
            CreateIndex("dbo.WordWords", "Word_Id");
            AddForeignKey("dbo.WordWords", "Word_Id1", "dbo.Words", "Id");
            AddForeignKey("dbo.WordWords", "Word_Id", "dbo.Words", "Id");
        }
    }
}
