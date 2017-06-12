using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Framework.Core;
using DictionariesSystem.Framework.Core.Exceptions;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.EngineTests
{
    [TestFixture]
    public class Start_Should
    {
        [Test]
        public void CallReaderReadLineMethod()
        {
            // arrange
            string commandMessage = "message";
            string terminateCommand = "Exit";

            var readerMock = new Mock<IReader>();
            readerMock.Setup(x => x.ReadLine()).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Returns(commandMessage);

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            readerMock.Verify(x => x.ReadLine(), Times.Once);
        }

        [Test]
        public void TerminateOperations_WhenThePassedCommandIsTerminationCommand()
        {
            // arrange
            string commandMessage = "message";
            string terminateCommand = "Exit";

            var readerMock = new Mock<IReader>();
            readerMock.Setup(x => x.ReadLine()).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Returns(commandMessage);

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            readerMock.Verify(x => x.ReadLine(), Times.Once);
            processorMock.Verify(x => x.ProcessCommand(It.IsAny<string>()), Times.Never);
            writerMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Never);
            loggerMock.Verify(x => x.Log(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void CallProcessorProcessCommandMethod()
        {
            // arrange
            string command = "command";
            string commandMessage = "message";
            string terminateCommand = "Exit";

            var readerMock = new Mock<IReader>();
            readerMock.SetupSequence(x => x.ReadLine()).Returns(command).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Returns(commandMessage);

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            processorMock.Verify(x => x.ProcessCommand(It.Is<string>(y => y == command)), Times.Once);
        }

        [Test]
        public void CallWriterWriteLineMethod()
        {
            // arrange
            string command = "command";
            string commandMessage = "message";
            string terminateCommand = "Exit";

            var readerMock = new Mock<IReader>();
            readerMock.SetupSequence(x => x.ReadLine()).Returns(command).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Returns(commandMessage);

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            writerMock.Verify(x => x.WriteLine(It.Is<string>(y => y == commandMessage)), Times.Once);
        }

        [Test]
        public void CallWriterWriteLineMethod_WhenUserAuthenticationExceptionIsThrown()
        {
            // arrange
            string command = "command";
            string terminateCommand = "Exit";
            string exceptionMessage = "message";

            var readerMock = new Mock<IReader>();
            readerMock.SetupSequence(x => x.ReadLine()).Returns(command).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Throws(new UserAuthenticationException(exceptionMessage));

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            writerMock.Verify(x => x.WriteLine(It.Is<string>(y => y == exceptionMessage)), Times.Once);
        }

        [Test]
        public void CallWriterWriteLineMethod_WhenInvalidCommandExceptionIsThrown()
        {
            // arrange
            string command = "command";
            string terminateCommand = "Exit";
            string exceptionMesssage = "message";
            string invalidCommandMessage = "Invalid command!";

            var readerMock = new Mock<IReader>();
            readerMock.SetupSequence(x => x.ReadLine()).Returns(command).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Throws(new InvalidCommandException(exceptionMesssage, new Exception()));

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            writerMock.Verify(x => x.WriteLine(It.Is<string>(y => y == invalidCommandMessage)), Times.Once);
        }

        [Test]
        public void CallWriterAndLogger_WhenGenericExceptionIsThrown()
        {
            // arrange
            string command = "command";
            string terminateCommand = "Exit";
            string exceptionMessage = "message";
            string unexpectedExceptionMessage = "Unexpected exception happened..";


            var readerMock = new Mock<IReader>();
            readerMock.SetupSequence(x => x.ReadLine()).Returns(command).Returns(terminateCommand);

            var processorMock = new Mock<ICommandProcessor>();
            processorMock.Setup(x => x.ProcessCommand(It.IsAny<string>())).Throws(new Exception(exceptionMessage));

            var writerMock = new Mock<IWriter>();
            writerMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(It.IsAny<string>()));

            var engine = new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object);

            // act
            engine.Start();

            // assert
            writerMock.Verify(x => x.WriteLine(It.Is<string>(y => y == unexpectedExceptionMessage)), Times.Once);
            loggerMock.Verify(x => x.Log(It.Is<string>(y => y == exceptionMessage)), Times.Once);
        }
    }
}
