using DictionariesSystem.Models.Dictionaries.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Word
    {
        private const int MaxNameLength = 40;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [Index(IsUnique =true)]
        public string Name { get; set; }

        public SpeechPart SpeechPart { get; set; }

        public int DictionaryId { get; set; }

        public virtual Dictionary Dictionary { get; set; }
        
        public virtual ICollection<Meaning> Meanings { get; set; }
    }
}