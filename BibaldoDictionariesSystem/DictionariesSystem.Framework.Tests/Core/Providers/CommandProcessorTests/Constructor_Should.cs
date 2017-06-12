using NUnit.Framework;
using Moq;
using DictionariesSystem.Contracts.Core.Providers;
using System;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Contracts.Core.Factories;

namespace DictionariesSystem.Framework.Tests.Core.Providers.CommandProcessorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenPassedCommandFactoryIsNull()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>(() => new CommandProcessor(null));
        }

        [Test]
        public void NotThrow_WhenPassedCommandFactoryIsNotNull()
        {
            // arrange & act & assert
            Assert.DoesNotThrow(() => new CommandProcessor(new Mock<ICommandFactory>().Object));
        }
    }
}
