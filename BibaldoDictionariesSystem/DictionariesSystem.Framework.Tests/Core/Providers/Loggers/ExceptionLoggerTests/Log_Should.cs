using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Models.Logs;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Providers.Loggers.ExceptionLoggerTests
{
    [TestFixture]
    public class Log_Should
    {
        [Test]
        public void CallFactoryGetExceptionLogMethod()
        {
            // arrange
            string exception = "exception";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<ExceptionLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new ExceptionLogger(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            // act
            userLogger.Log(exception);

            // assert
            factoryMock.Verify(x => x.GetExceptionLog(It.Is<string>(y => y == exception), It.Is<DateTime>(y => y == date)), Times.Once);
        }

        [Test]
        public void CallDateProviderGetDateMethod()
        {
            // arrange
            string exception = "exception";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<ExceptionLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new ExceptionLogger(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            // act
            userLogger.Log(exception);

            // assert
            dateProviderMock.Verify(x => x.GetDate(), Times.Once);
        }

        [Test]
        public void CallRepositoryAddMethod()
        {
            // arrange
            string exception = "exception";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<ExceptionLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new ExceptionLogger(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            // act
            userLogger.Log(exception);

            // assert
            repositoryMock.Verify(x => x.Add(It.IsAny<ExceptionLog>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkSaveChangesMethod()
        {
            // arrange
            string exception = "exception";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<ExceptionLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<ExceptionLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new ExceptionLogger(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            // act
            userLogger.Log(exception);

            // assert
            unitOfWorkMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
