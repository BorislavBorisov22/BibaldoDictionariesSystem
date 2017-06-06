namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeduniqueconstraintsfornamesoflanguagesandwords : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Languages", "Name", unique: true);
            CreateIndex("dbo.Words", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Words", new[] { "Name" });
            DropIndex("dbo.Languages", new[] { "Name" });
        }
    }
}
