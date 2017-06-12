using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Framework.Core.Commands.Update;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Update.ImportWordsFromFileCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedWordsImporterFactoryIsNull()
        {
            // arrange and act and assert
            Assert.Throws<ArgumentNullException>(() => new ImportWordsFromFileCommand(null));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            // arrange
            var importersFactory = new Mock<IWordsImporterFactory>();

            // act and assert
            Assert.DoesNotThrow(() => new ImportWordsFromFileCommand(importersFactory.Object));
        }
    }
}
