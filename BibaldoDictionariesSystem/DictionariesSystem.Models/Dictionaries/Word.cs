using DictionariesSystem.Models.Dictionaries.Enums;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public SpeechPart SpeechPart { get; set; }

        public int DictionaryId { get; set; }

        public virtual Dictionary Dictionary { get; set; }
    }
}