using DictionariesSystem.Models.Users;

namespace DictionariesSystem.Contracts.Core.Providers
{
    public interface IUserProvider
    {
        User LoggedUser { get; }

        void Login(string username, string password);

        void Register(string username, string password);

        bool IsLogged { get; }

        void Logout();
    }
}
