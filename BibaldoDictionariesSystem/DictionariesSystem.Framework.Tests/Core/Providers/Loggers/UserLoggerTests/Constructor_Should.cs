using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Models.Logs;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Providers.Loggers.UserLoggerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenPassedRepositoryIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<UserLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userProviderMock = new Mock<IUserProvider>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new UserLogger(null, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenPassedUnitOfWorkIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<UserLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userProviderMock = new Mock<IUserProvider>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                 () => new UserLogger(repositoryMock.Object, null, userProviderMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenPassedUserProviderIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<UserLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userProviderMock = new Mock<IUserProvider>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenPassedFactoryIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<UserLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userProviderMock = new Mock<IUserProvider>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenPassedParametersAreCorrect()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<UserLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userProviderMock = new Mock<IUserProvider>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.DoesNotThrow(
                () => new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object));
        }
    }
}
