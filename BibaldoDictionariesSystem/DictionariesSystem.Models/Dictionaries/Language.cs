﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Language
    {
        [ForeignKey("Dictionary")]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Dictionary Dictionary { get; set; }
    }
}