using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Dictionary
    {
        public Dictionary()
        {
            this.CreatedOn = DateTime.Now;
            this.Words = new HashSet<Word>();
        }

        private const int MaxAuthorLength = 20;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxAuthorLength)]
        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<Word> Words { get; set; }
    }
}
