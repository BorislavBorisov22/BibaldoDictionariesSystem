using DictionariesSystem.Models.Dictionaries;
using System.Data.Entity;

namespace DictionariesSystem.Data.Dictionaries
{
    public class DictionariesDbContext : DbContext
    {
        private const string ConnectionStringName = "DictionariesDB";

        public DictionariesDbContext()
            : base(ConnectionStringName)
        {
        }
        public virtual IDbSet<Synonym> Synonyms { get; set; }

        public virtual IDbSet<Dictionary> Dictionaries  { get; set; }

        public virtual IDbSet<Language> Languages { get; set; }

        public virtual IDbSet<Meaning> Meanings { get; set; }

        public virtual IDbSet<Word> Words { get; set; }
    }
}
