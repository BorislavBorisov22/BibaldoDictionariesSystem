using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Exceptions;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests.Fakes;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;

namespace DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests
{
    [TestFixture]
    public class Logout_Should
    {
        [Test]
        public void ThrowUserAuthenticationException_WhenTheUserHasAlreadyLoggedOut()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act and assert 
            Assert.Throws<UserAuthenticationException>(() => userProvider.Logout());
        }

        [Test]
        public void SetLoggedUserToNull_WhenMethodIsCalledWithALoggedInUser()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();
         
            var userProvider = new FakeUserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            var user = new User();
            userProvider.SetLoggedUser(user);

            // act
            userProvider.Logout();

            // assert
            Assert.IsNull(userProvider.LoggedUser);
        }
    }
}
