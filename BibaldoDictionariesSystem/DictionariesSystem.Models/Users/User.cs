namespace DictionariesSystem.Models.Users
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Badges = new HashSet<Badge>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Passhash { get; set; }

        public int ContributionsCount { get; set; }

        public virtual ICollection<Badge> Badges { get; set; }
    }
}
