using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;
using System.Data.Entity;

namespace DictionariesSystem.Data.Dictionaries
{

    public class DictionariesDbContext : DbContext
    {
        public DictionariesDbContext()
            : base("DictionaryDB")
        {

        }

        public DbSet<Dictionary> Dictionaries  { get; set; }

        //public DbSet<Language> Languages { get; set; }

        public DbSet<Meaning> Meanings { get; set; }

        public DbSet<Word> Words { get; set; }
    }
}
