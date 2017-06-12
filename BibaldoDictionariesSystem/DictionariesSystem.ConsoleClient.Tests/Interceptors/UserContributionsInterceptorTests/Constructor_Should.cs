using DictionariesSystem.ConsoleClient.Interceptors;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.UserContributionsInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedUserProviderIsNull()
        {
            // arrange
            IUserProvider userProvider = null;
            var usersRepostiroy = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UserContributionsInterceptor(
                    userProvider,
                    usersRepostiroy.Object,
                    badgesRepository.Object,
                    unitOfWork.Object
                    ));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUserRepositoryIsNull()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            IRepository<User> usersRepostiroy = null;
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UserContributionsInterceptor(
                    userProvider.Object,
                    usersRepostiroy,
                    badgesRepository.Object,
                    unitOfWork.Object
                    ));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedBadgesRepositroyIsNull()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            var usersRepostiroy = new Mock<IRepository<User>>();
            IRepository<Badge> badgesRepository = null;
            var unitOfWork = new Mock<IUnitOfWork>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UserContributionsInterceptor(
                    userProvider.Object,
                    usersRepostiroy.Object,
                    badgesRepository,
                    unitOfWork.Object
                    ));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUnitOfWorkIsNull()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            var usersRepostiroy = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            IUnitOfWork unitOfWork = null;

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UserContributionsInterceptor(
                    userProvider.Object,
                    usersRepostiroy.Object,
                    badgesRepository.Object,
                    unitOfWork
                    ));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            var userProvider = new Mock<IUserProvider>();
            var usersRepostiroy = new Mock<IRepository<User>>();
            var badgesRepository = new Mock<IRepository<Badge>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            // act and assert
            Assert.DoesNotThrow
                (() => new UserContributionsInterceptor(
                    userProvider.Object,
                    usersRepostiroy.Object,
                    badgesRepository.Object,
                    unitOfWork.Object
                    ));
        }
    }
}