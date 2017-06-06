namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(nullable: false, maxLength: 20),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        SpeechPart = c.Int(nullable: false),
                        DictionaryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dictionaries", t => t.DictionaryId, cascadeDelete: true)
                .Index(t => t.DictionaryId);
            
            CreateTable(
                "dbo.Meanings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Synonyms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstWord_Id = c.Int(),
                        SecondWord_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Words", t => t.FirstWord_Id)
                .ForeignKey("dbo.Words", t => t.SecondWord_Id)
                .Index(t => t.FirstWord_Id)
                .Index(t => t.SecondWord_Id);
            
            CreateTable(
                "dbo.MeaningWords",
                c => new
                    {
                        Meaning_Id = c.Int(nullable: false),
                        Word_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meaning_Id, t.Word_Id })
                .ForeignKey("dbo.Meanings", t => t.Meaning_Id, cascadeDelete: true)
                .ForeignKey("dbo.Words", t => t.Word_Id, cascadeDelete: true)
                .Index(t => t.Meaning_Id)
                .Index(t => t.Word_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Synonyms", "SecondWord_Id", "dbo.Words");
            DropForeignKey("dbo.Synonyms", "FirstWord_Id", "dbo.Words");
            DropForeignKey("dbo.MeaningWords", "Word_Id", "dbo.Words");
            DropForeignKey("dbo.MeaningWords", "Meaning_Id", "dbo.Meanings");
            DropForeignKey("dbo.Words", "DictionaryId", "dbo.Dictionaries");
            DropForeignKey("dbo.Languages", "Id", "dbo.Dictionaries");
            DropIndex("dbo.MeaningWords", new[] { "Word_Id" });
            DropIndex("dbo.MeaningWords", new[] { "Meaning_Id" });
            DropIndex("dbo.Synonyms", new[] { "SecondWord_Id" });
            DropIndex("dbo.Synonyms", new[] { "FirstWord_Id" });
            DropIndex("dbo.Words", new[] { "DictionaryId" });
            DropIndex("dbo.Languages", new[] { "Id" });
            DropTable("dbo.MeaningWords");
            DropTable("dbo.Synonyms");
            DropTable("dbo.Meanings");
            DropTable("dbo.Words");
            DropTable("dbo.Languages");
            DropTable("dbo.Dictionaries");
        }
    }
}
