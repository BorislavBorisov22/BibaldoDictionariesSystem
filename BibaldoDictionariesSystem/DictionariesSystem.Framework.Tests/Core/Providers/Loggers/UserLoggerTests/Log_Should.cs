using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Models.Logs;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Providers.Loggers.UserLoggerTests
{
    [TestFixture]
    public class Log_Should
    {
        [Test]
        public void CallFactoryGetUserLogMethod()
        {
            // arrange
            string command = "command";
            string username = "username";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<UserLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<UserLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var userMock = new User() { Username = username };
            var userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.LoggedUser).Returns(userMock);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object);

            // act
            userLogger.Log(command);

            // assert
            factoryMock.Verify(x => x.GetUserLog(
                It.Is<string>(y => y == username), It.Is<string>(y => y == command), It.Is<DateTime>(y => y == date)), Times.Once);
        }

        [Test]
        public void CallUserProviderLoggedUserMethod()
        {
            // arrange
            string command = "command";
            string username = "username";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<UserLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<UserLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var userMock = new User() { Username = username };
            var userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.LoggedUser).Returns(userMock);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object);

            // act
            userLogger.Log(command);

            // assert
            userProviderMock.Verify(x => x.LoggedUser, Times.Once);
        }

        [Test]
        public void CallDateProviderGetDateMethod()
        {
            // arrange
            string command = "command";
            string username = "username";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<UserLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<UserLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var userMock = new User() { Username = username };
            var userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.LoggedUser).Returns(userMock);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object);

            // act
            userLogger.Log(command);

            // assert
            dateProviderMock.Verify(x => x.GetDate(), Times.Once);
        }

        [Test]
        public void CallRepositoryAddMethod()
        {
            // arrange
            string command = "command";
            string username = "username";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<UserLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<UserLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var userMock = new User() { Username = username };
            var userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.LoggedUser).Returns(userMock);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object);

            // act
            userLogger.Log(command);

            // assert
            repositoryMock.Verify(x => x.Add(It.IsAny<UserLog>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkSaveChangesMethod()
        {
            // arrange
            string command = "command";
            string username = "username";
            DateTime date = new DateTime(2017, 6, 12);

            var repositoryMock = new Mock<IRepository<UserLog>>();
            repositoryMock.Setup(x => x.Add(It.IsAny<UserLog>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChanges());

            var userMock = new User() { Username = username };
            var userProviderMock = new Mock<IUserProvider>();
            userProviderMock.Setup(x => x.LoggedUser).Returns(userMock);

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.GetDate()).Returns(date);
            DateProvider.Provider = dateProviderMock.Object;

            var factoryMock = new Mock<ILogsFactory>();
            factoryMock.Setup(x => x.GetUserLog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var userLogger = new UserLogger(repositoryMock.Object, unitOfWorkMock.Object, userProviderMock.Object, factoryMock.Object);

            // act
            userLogger.Log(command);

            // assert
            unitOfWorkMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
