using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        /*public int DictionaryId { get; set; }

        public virtual Dictionary Dictionary { get; set; }*/
    }
}