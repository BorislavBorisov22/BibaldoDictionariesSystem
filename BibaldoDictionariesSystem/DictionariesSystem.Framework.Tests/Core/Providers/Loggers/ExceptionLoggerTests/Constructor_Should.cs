using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Models.Logs;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Providers.Loggers.ExceptionLoggerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenPassedRepositoryIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new ExceptionLogger(null, unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenPassedUnitOfWorkIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new ExceptionLogger(repositoryMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenPassedFactoryIsNull()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new ExceptionLogger(repositoryMock.Object, unitOfWorkMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenArgumentsAreCorrect()
        {
            // arrange
            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<ILogsFactory>();

            // act & assert
            Assert.DoesNotThrow(
                () => new ExceptionLogger(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object));
        }
    }
}
