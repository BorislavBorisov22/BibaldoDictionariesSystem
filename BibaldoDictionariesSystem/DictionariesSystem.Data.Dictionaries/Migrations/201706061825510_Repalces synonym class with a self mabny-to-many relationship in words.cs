namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Repalcessynonymclasswithaselfmabnytomanyrelationshipinwords : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Synonyms", "FirstWord_Id", "dbo.Words");
            DropForeignKey("dbo.Synonyms", "SecondWord_Id", "dbo.Words");
            DropIndex("dbo.Synonyms", new[] { "FirstWord_Id" });
            DropIndex("dbo.Synonyms", new[] { "SecondWord_Id" });
            CreateTable(
                "dbo.WordWords",
                c => new
                    {
                        Word_Id = c.Int(nullable: false),
                        Word_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Word_Id, t.Word_Id1 })
                .ForeignKey("dbo.Words", t => t.Word_Id)
                .ForeignKey("dbo.Words", t => t.Word_Id1)
                .Index(t => t.Word_Id)
                .Index(t => t.Word_Id1);
            
            DropTable("dbo.Synonyms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Synonyms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstWord_Id = c.Int(),
                        SecondWord_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.WordWords", "Word_Id1", "dbo.Words");
            DropForeignKey("dbo.WordWords", "Word_Id", "dbo.Words");
            DropIndex("dbo.WordWords", new[] { "Word_Id1" });
            DropIndex("dbo.WordWords", new[] { "Word_Id" });
            DropTable("dbo.WordWords");
            CreateIndex("dbo.Synonyms", "SecondWord_Id");
            CreateIndex("dbo.Synonyms", "FirstWord_Id");
            AddForeignKey("dbo.Synonyms", "SecondWord_Id", "dbo.Words", "Id");
            AddForeignKey("dbo.Synonyms", "FirstWord_Id", "dbo.Words", "Id");
        }
    }
}
