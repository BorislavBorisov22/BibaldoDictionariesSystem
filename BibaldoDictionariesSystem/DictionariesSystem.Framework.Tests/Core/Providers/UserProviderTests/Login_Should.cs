using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Exceptions;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests.Fakes;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Providers.UserProviderTests
{
    [TestFixture]
    public class Login_Should
    {
        [Test]
        public void ThrowUserAuthenticationException_WhenAUserIsAlreadyLoggedIn()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var userProvider = new FakeUserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            var user = new User();
            userProvider.SetLoggedUser(user);

            // act and assert
            Assert.Throws<UserAuthenticationException>(() => userProvider.Login("SomeUserName", "SomeOtherUserName"));
        }

        [Test]
        public void ThrowUserAuthenticationException_WhenNoUserWithTheProviderCredentialsIsFound()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            userRepository
                .Setup(x => x.All(y => y.Username == It.IsAny<string>() && y.Passhash == It.IsAny<string>()))
                .Returns(new List<User>());

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act and assert 
            Assert.Throws<UserAuthenticationException>(() => userProvider.Login("Username", "Passhash"));
        }

        [Test]
        public void SetLoggedUserCorrectly_WhenPassedUserCredentialsAreValid()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var user = new User() { Username = "Ivancho", Passhash = "SomePasss" };
            var users = new List<User>()
            {
                user
            };

            userRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);


            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act 
            userProvider.Login("Username", "Passhash");

            // assert
            Assert.AreSame(userProvider.LoggedUser, user);
        }
    }
}
