using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionariesSystem.Models.Dictionaries
{
    public class Synonym
    {
        [Key]
        public int Id { get; set; }
        
        public Word FirstWord { get; set; }
        
        public Word SecondWord { get; set; }
    }
}
