using DictionariesSystem.Models.Users;

namespace DictionariesSystem.Contracts.Core.Factories
{
    public interface IUserFactory
    {
        User GetUser(string username, string password);

        Badge GetBadge(string name, int requiredContributions);
    }
}
