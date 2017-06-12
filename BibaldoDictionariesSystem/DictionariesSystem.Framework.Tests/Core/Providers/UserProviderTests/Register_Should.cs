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
    public class Register_Should
    {
        [Test]
        public void ThrowUserAuthenticationException_WhenThereIsAUserLoggedIn()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            userRepository
                .Setup(x => x.All(y => y.Username == It.IsAny<string>() && y.Passhash == It.IsAny<string>()))
                .Returns(new List<User>());

            var userProvider = new FakeUserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            var loggedUser = new User();
            userProvider.SetLoggedUser(loggedUser);

            // act and assert 
            Assert.Throws<UserAuthenticationException>(() => userProvider.Register("Username", "Passhash"));
        }

        [Test]
        public void ThrowUserAuthenticationException_WhenAUserWithThePassedUsernameAlreadyExists()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var existingUser = new User() { Username = "SomeUsername", Passhash = "SomePasshash" };
            var users = new List<User>() { existingUser};

            userRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act and assert 
            Assert.Throws<UserAuthenticationException>(() => userProvider.Register(existingUser.Username, existingUser.Passhash));
        }

        [Test]
        public void CallUserFactoryWithCorrectArguments_WhenNoSuchUserExistsInUsersRepository()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var users = new List<User>();

            userRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            string username = "SomeUsername";
            string passhash = "SomePass";

            // act
            userProvider.Register(username, passhash);

            // assert 
            userFactory.Verify(x => x.GetUser(username, passhash), Times.Once);
        }

        [Test]
        public void CallUserRepositoryAddMethodWithTheNewlyCreateUser_WhenNoSuchUserExists()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var users = new List<User>();

            userRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);

            string username = "SomeUsername";
            string passhash = "SomePass";

            var newUser = new User() { Username = username, Passhash = passhash };
            userFactory.Setup(x => x.GetUser(username, passhash)).Returns(newUser);

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act
            userProvider.Register(username, passhash);

            // assert 
            userRepository.Verify(x => x.Add(It.Is<User>(u => u == newUser)), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkSaveChangesMethod_WhenUserIssuccessfullyAddedToUsersRepository()
        {
            // arrange 
            var userRepository = new Mock<IRepository<User>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userFactory = new Mock<IUserFactory>();

            var users = new List<User>();

            userRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);

            string username = "SomeUsername";
            string passhash = "SomePass";

            var newUser = new User() { Username = username, Passhash = passhash };
            userFactory.Setup(x => x.GetUser(username, passhash)).Returns(newUser);

            var userProvider = new UserProvider(userRepository.Object, unitOfWork.Object, userFactory.Object);

            // act
            userProvider.Register(username, passhash);

            // assert 
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}