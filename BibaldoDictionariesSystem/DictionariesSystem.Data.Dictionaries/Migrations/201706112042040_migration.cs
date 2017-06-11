namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MadeWordsDictionaryIdNullable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MeaningWords", newName: "WordMeanings");
            DropForeignKey("dbo.Languages", "Id", "dbo.Dictionaries");
            DropForeignKey("dbo.Words", "DictionaryId", "dbo.Dictionaries");
            DropIndex("dbo.Words", new[] { "DictionaryId" });
            DropPrimaryKey("dbo.WordMeanings");
            AddColumn("dbo.Meanings", "Dictionary_Id", c => c.Int());
            AlterColumn("dbo.Words", "DictionaryId", c => c.Int());
            AddPrimaryKey("dbo.WordMeanings", new[] { "Word_Id", "Meaning_Id" });
            CreateIndex("dbo.Meanings", "Dictionary_Id");
            CreateIndex("dbo.Words", "DictionaryId");
            AddForeignKey("dbo.Meanings", "Dictionary_Id", "dbo.Dictionaries", "Id");
            AddForeignKey("dbo.Languages", "Id", "dbo.Dictionaries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Words", "DictionaryId", "dbo.Dictionaries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "DictionaryId", "dbo.Dictionaries");
            DropForeignKey("dbo.Languages", "Id", "dbo.Dictionaries");
            DropForeignKey("dbo.Meanings", "Dictionary_Id", "dbo.Dictionaries");
            DropIndex("dbo.Words", new[] { "DictionaryId" });
            DropIndex("dbo.Meanings", new[] { "Dictionary_Id" });
            DropPrimaryKey("dbo.WordMeanings");
            AlterColumn("dbo.Words", "DictionaryId", c => c.Int(nullable: false));
            DropColumn("dbo.Meanings", "Dictionary_Id");
            AddPrimaryKey("dbo.WordMeanings", new[] { "Meaning_Id", "Word_Id" });
            CreateIndex("dbo.Words", "DictionaryId");
            AddForeignKey("dbo.Words", "DictionaryId", "dbo.Dictionaries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Languages", "Id", "dbo.Dictionaries", "Id");
            RenameTable(name: "dbo.WordMeanings", newName: "MeaningWords");
        }
    }
}
