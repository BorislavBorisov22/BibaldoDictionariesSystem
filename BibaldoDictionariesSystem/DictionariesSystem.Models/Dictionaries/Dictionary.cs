using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Dictionary
    {
        private const int MaxAuthorLength = 20;
        private const int MaxTitleLength = 30;

        public Dictionary()
        {
            this.CreatedOn = DateTime.Now;
            this.Words = new HashSet<Word>();
            this.Meanings = new HashSet<Meaning>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MaxAuthorLength)]
        public string Author { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<Word> Words { get; set; }

        public virtual ICollection<Meaning> Meanings { get; set; }
    }
}
