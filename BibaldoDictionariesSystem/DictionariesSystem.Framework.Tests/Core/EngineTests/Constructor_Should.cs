using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Framework.Core;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.EngineTests
{
    [TestFixture]
    public class Constructor_Should
    {
        public void ThrowArgumentNullException_WhenProcessorIsNull()
        {
            // arrange
            var processorMock = new Mock<ICommandProcessor>();
            var writerMock = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var loggerMock = new Mock<ILogger>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new Engine(null, writerMock.Object, readerMock.Object, loggerMock.Object));
        }

        public void ThrowArgumentNullException_WhenWriterIsNull()
        {
            // arrange
            var processorMock = new Mock<ICommandProcessor>();
            var writerMock = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var loggerMock = new Mock<ILogger>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new Engine(processorMock.Object, null, readerMock.Object, loggerMock.Object));
        }

        public void ThrowArgumentNullException_WhenReaderIsNull()
        {
            // arrange
            var processorMock = new Mock<ICommandProcessor>();
            var writerMock = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var loggerMock = new Mock<ILogger>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new Engine(processorMock.Object, writerMock.Object,null, loggerMock.Object));
        }

        public void ThrowArgumentNullException_WhenLoggerIsNull()
        {
            // arrange
            var processorMock = new Mock<ICommandProcessor>();
            var writerMock = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var loggerMock = new Mock<ILogger>();

            // act & assert
            Assert.Throws<ArgumentNullException>(
                () => new Engine(processorMock.Object, writerMock.Object, readerMock.Object, null));
        }

        public void NotThrow_WhenPassedParametersAreCorrect()
        {
            // arrange
            var processorMock = new Mock<ICommandProcessor>();
            var writerMock = new Mock<IWriter>();
            var readerMock = new Mock<IReader>();
            var loggerMock = new Mock<ILogger>();

            // act & assert
            Assert.DoesNotThrow(
                () => new Engine(processorMock.Object, writerMock.Object, readerMock.Object, loggerMock.Object));
        }
    }
}
