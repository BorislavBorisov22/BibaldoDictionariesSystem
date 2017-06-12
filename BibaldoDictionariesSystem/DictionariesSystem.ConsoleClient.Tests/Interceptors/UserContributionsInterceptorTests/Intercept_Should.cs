using DictionariesSystem.ConsoleClient.Interceptors;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Users;
using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.UserContributionsInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void IncrementUsersContrbitions_WhenSuchUserExists()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            var usersRepository = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var user = new User() { Id = 1 };
            var users = new List<User>() { user };
            var badges = new List<Badge>();

            userProvider.Setup(x => x.LoggedUser).Returns(user);
            usersRepository.Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);
            badgesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Badge, bool>>>()))
              .Returns(badges);

            var userContributionsIntereptor = new UserContributionsInterceptor(
                userProvider.Object,
                usersRepository.Object,
                badgesRepository.Object,
                unitOfWork.Object);

            var invocation = new Mock<IInvocation>();

            // act
            userContributionsIntereptor.Intercept(invocation.Object);

            // assert
            Assert.AreEqual(1, user.ContributionsCount);
        }

        [Test]
        public void NotAddBadgeToUser_WhenUserDoesNotHaveRequiredContributions()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            var usersRepository = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var user = new User() { Id = 1, ContributionsCount = 10 };
            var users = new List<User>() { user };

            var badge = new Badge() { RequiredContributions = 20 };
            var badges = new List<Badge>() { badge };

            userProvider.Setup(x => x.LoggedUser).Returns(user);
            usersRepository.Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);
            badgesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Badge, bool>>>()))
              .Returns(badges);

            var userContributionsIntereptor = new UserContributionsInterceptor(
                userProvider.Object,
                usersRepository.Object,
                badgesRepository.Object,
                unitOfWork.Object);

            var invocation = new Mock<IInvocation>();

            // act
            userContributionsIntereptor.Intercept(invocation.Object);

            // assert
            Assert.AreEqual(1, user.Badges.Count);
        }

        [Test]
        public void NotAddBadgeToUser_WhenUserHasRequiredContributionsAlreadyHasThatBadge()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            var usersRepository = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var user = new User() { Id = 1, ContributionsCount = 30 };
            var users = new List<User>() { user };

            var badge = new Badge() { RequiredContributions = 20, Name = "SomeName" };
            var badges = new List<Badge>() { badge };
            user.Badges.Add(badge);

            userProvider.Setup(x => x.LoggedUser).Returns(user);
            usersRepository.Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);
            badgesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Badge, bool>>>()))
              .Returns(badges);

            var userContributionsIntereptor = new UserContributionsInterceptor(
                userProvider.Object,
                usersRepository.Object,
                badgesRepository.Object,
                unitOfWork.Object);

            var invocation = new Mock<IInvocation>();

            // act
            userContributionsIntereptor.Intercept(invocation.Object);

            // assert
            Assert.AreEqual(1, user.Badges.Count);
        }

        [Test]
        public void AddBadgeToUser_WhenUserHasRequiredContributionsAndHasntStillAquiredBadge()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            var usersRepository = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var user = new User() { Id = 1, ContributionsCount = 30 };
            var users = new List<User>() { user };

            var badge = new Badge() { RequiredContributions = 20, Name = "SomeName" };
            var badges = new List<Badge>() { badge };

            userProvider.Setup(x => x.LoggedUser).Returns(user);
            usersRepository.Setup(x => x.All(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);
            badgesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Badge, bool>>>()))
              .Returns(badges);

            var userContributionsIntereptor = new UserContributionsInterceptor(
                userProvider.Object,
                usersRepository.Object,
                badgesRepository.Object,
                unitOfWork.Object);

            var invocation = new Mock<IInvocation>();

            // act
            userContributionsIntereptor.Intercept(invocation.Object);

            // assert
            Assert.AreSame(badge, user.Badges.First());
        }
    }
}
