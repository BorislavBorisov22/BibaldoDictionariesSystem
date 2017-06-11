using DictionariesSystem.Data.Dictionaries.Migrations;
using DictionariesSystem.Models.Dictionaries;
using System.Data.Entity;

namespace DictionariesSystem.Data.Dictionaries
{
    public class DictionariesDbContext : DbContext
    {
        private const string ConnectionStringName = "DictionariesDb";

        public DictionariesDbContext()
            : base(ConnectionStringName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DictionariesDbContext, Configuration>());
        }

        public virtual IDbSet<Dictionary> Dictionaries { get; set; }

        public virtual IDbSet<Language> Languages { get; set; }

        public virtual IDbSet<Meaning> Meanings { get; set; }

        public virtual IDbSet<Word> Words { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dictionary>()
                .HasOptional(x => x.Language)
                .WithRequired(x => x.Dictionary)
                .WillCascadeOnDelete(true);
        }
    }
}