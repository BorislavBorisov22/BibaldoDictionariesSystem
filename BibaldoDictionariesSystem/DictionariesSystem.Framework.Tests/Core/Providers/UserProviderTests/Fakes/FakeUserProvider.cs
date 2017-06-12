using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Models.Users;

namespace DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests.Fakes
{
    public class FakeUserProvider : UserProvider
    {
        public FakeUserProvider(IRepository<User> usersRepository, IUnitOfWork unitOfWork, IUserFactory userFactory) 
            : base(usersRepository, unitOfWork, userFactory)
        {
        }

        public void SetLoggedUser(User user)
        {
            this.LoggedUser = user;
        }
    }
}
