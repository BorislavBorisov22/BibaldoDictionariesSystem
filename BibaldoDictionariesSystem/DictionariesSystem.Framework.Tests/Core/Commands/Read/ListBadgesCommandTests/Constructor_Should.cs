using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Framework.Core.Commands.Read;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Read.ListBadgesCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_WhenNoUserProviderIsProvided()
        {
            Assert.Throws<ArgumentNullException>(() => new ListBadgesCommand(null));
        }

        [Test]
        public void NotThrow_WhenUserProviderIsProvided()
        {
            var userProvider = new Mock<IUserProvider>();
            Assert.DoesNotThrow(() => new ListBadgesCommand(userProvider.Object));
        }
    }
}
