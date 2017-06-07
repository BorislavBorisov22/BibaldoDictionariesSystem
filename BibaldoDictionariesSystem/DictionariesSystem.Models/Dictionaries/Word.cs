using DictionariesSystem.Models.Dictionaries.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Word
    {
        private const int MaxNameLength = 40;

        public Word()
        {
            this.Meanings = new HashSet<Meaning>();
            this.ChildWords = new HashSet<Word>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public SpeechPart SpeechPart { get; set; }
        
        public int DictionaryId { get; set; }

        public virtual Dictionary Dictionary { get; set; }
        
        public virtual ICollection<Meaning> Meanings { get; set; }

        public int? RootWordId { get; set; }

        public virtual Word RootWord { get; set; }

        public virtual ICollection<Word> ChildWords  { get; set; }
    }
}