using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Models.Users;

namespace DictionariesSystem.Framework.Core.Factories
{
    public class UserFactory : IUserFactory
    {
        public Badge GetBadge(string name, int requiredContributions)
        {
            return new Badge() { Name = name, RequiredContributions = requiredContributions };
        }

        public User GetUser(string username, string password)
        {
            return new User() { Username = username, Passhash = password };
        }
    }
}
