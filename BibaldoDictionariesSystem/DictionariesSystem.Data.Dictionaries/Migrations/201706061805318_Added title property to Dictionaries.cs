namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedtitlepropertytoDictionaries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dictionaries", "Title", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dictionaries", "Title");
        }
    }
}
