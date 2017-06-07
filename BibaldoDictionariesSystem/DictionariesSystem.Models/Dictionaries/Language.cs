using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Language
    {
        private const int MaxNameLength = 20;

        [ForeignKey("Dictionary")]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual Dictionary Dictionary { get; set; }
    }
}