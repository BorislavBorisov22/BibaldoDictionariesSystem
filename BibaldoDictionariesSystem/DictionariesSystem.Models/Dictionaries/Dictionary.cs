using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Dictionary
    {
        [Key]
        public int Id { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Language Language { get; set; }

        public virtual IEnumerable<Word> Words { get; set; }
    }
}
