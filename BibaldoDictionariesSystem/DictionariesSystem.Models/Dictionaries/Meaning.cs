using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Meaning
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}