using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests.Fakes;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;

namespace DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests
{
    [TestFixture]
    public class LoggedUser_Should
    {
        [Test]
        public void ReturnNullValue_WhenNoUserHasBeenLogged()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act
            var loggedUser = userProvider.LoggedUser;

            // assert
            Assert.IsNull(loggedUser);
        }

        [Test]
        public void ReturnTheCorrectUser_WhenAUserHasBeenLoggedIn()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var userProvider = new FakeUserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            var user = new User();
            userProvider.SetLoggedUser(user);

            // act
            var loggedUser = userProvider.LoggedUser;

            // assert
            Assert.AreSame(loggedUser, userProvider.LoggedUser);
        }
    }
}
