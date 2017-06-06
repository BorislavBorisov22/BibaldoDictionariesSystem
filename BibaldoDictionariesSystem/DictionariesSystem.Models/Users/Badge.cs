using System.Collections.Generic;

namespace DictionariesSystem.Models.Users
{
    public class Badge
    {
        public Badge()
        {
            this.Users = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int RequiredContributions { get; set; }  

        public virtual ICollection<User> Users { get; set; }
    }
}