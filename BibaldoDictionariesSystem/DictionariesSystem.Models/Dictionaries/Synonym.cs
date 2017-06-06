namespace DictionariesSystem.Models.Dictionaries
{
    public class Synonym
    {   
        public int Id { get; set; }

        public virtual Word FirstWord { get; set; }
       
        public virtual Word SecondWord { get; set; }
    }
}
