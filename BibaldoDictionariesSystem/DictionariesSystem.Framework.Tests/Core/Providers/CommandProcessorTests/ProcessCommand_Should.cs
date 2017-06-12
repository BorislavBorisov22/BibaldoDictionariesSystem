using NUnit.Framework;
using Moq;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Contracts.Core.Factories;
using System;
using DictionariesSystem.Contracts.Core.Commands;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Tests.Core.Providers.CommandProcessorTests
{
    [TestFixture]
    public class ProcessCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedParameterIsNull()
        {
            // arrange
            var factoryMock = new Mock<ICommandFactory>();
            var processor = new CommandProcessor(factoryMock.Object);

            // act & assert
            Assert.Throws<ArgumentNullException>(() => processor.ProcessCommand(null));
        }

        [Test]
        public void CallCommandFactoryGetCommandMethod_WhenPassedParameterIsCorrect()
        {
            // arrange
            string commandName = "name";
            string passedCommand = "name param";
            string result = "execution result";

            var commandMock = new Mock<ICommand>();
            commandMock.Setup(x => x.Execute(It.IsAny<IList<string>>())).Returns(result);

            var factoryMock = new Mock<ICommandFactory>();
            factoryMock.Setup(x => x.GetCommand(It.IsAny<string>())).Returns(commandMock.Object);

            var processor = new CommandProcessor(factoryMock.Object);

            // act
            processor.ProcessCommand(passedCommand);

            // assert
            factoryMock.Verify(x => x.GetCommand(It.Is<string>(y => y == commandName)), Times.Once);
        }

        [Test]
        public void CallCommandExecuteMethod_WhenPassedParametersAreCorrect()
        {
            // arrange
            string passedCommand = "name param";
            string result = "execution result";

            var commandMock = new Mock<ICommand>();
            commandMock.Setup(x => x.Execute(It.IsAny<IList<string>>())).Returns(result);

            var factoryMock = new Mock<ICommandFactory>();
            factoryMock.Setup(x => x.GetCommand(It.IsAny<string>())).Returns(commandMock.Object);

            var processor = new CommandProcessor(factoryMock.Object);

            // act
            processor.ProcessCommand(passedCommand);

            // assert
            commandMock.Verify(x => x.Execute(It.Is<IList<string>>(y => y.Contains("param"))), Times.Once);
        }

        [Test]
        public void ReturnTheCommandResult_WhenPassedParametersAreCorrect()
        {
            // arrange
            string passedCommand = "name param";
            string result = "execution result";

            var commandMock = new Mock<ICommand>();
            commandMock.Setup(x => x.Execute(It.IsAny<IList<string>>())).Returns(result);

            var factoryMock = new Mock<ICommandFactory>();
            factoryMock.Setup(x => x.GetCommand(It.IsAny<string>())).Returns(commandMock.Object);

            var processor = new CommandProcessor(factoryMock.Object);

            // act
            var actual = processor.ProcessCommand(passedCommand);

            // assert
            Assert.AreEqual(result, actual);
        }
    }
}
