using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Read.ListBadgesCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ShouldThrow_WhenNoUserIsLoggedIn()
        {
            var userProvider = new Mock<IUserProvider>();
            var command = new ListBadgesCommand(userProvider.Object);

            Assert.Throws<ArgumentException>(() => command.Execute(new List<string>()));
        }

        [Test]
        public void ShouldReturnCorrectMessage_WhenTheUserHasNoBadges()
        {
            var userProvider = new Mock<IUserProvider>();
            var command = new ListBadgesCommand(userProvider.Object);
            var user = new User()
            {
                Username = "SomeUsername"
            };

            userProvider.Setup(u => u.IsLogged).Returns(true);
            userProvider.Setup(u => u.LoggedUser).Returns(user);

            string result = command.Execute(new List<string>());
            string expectedResult = $"{userProvider.Object.LoggedUser.Username} has no badges.\r\n";

            Assert.AreEqual(result, expectedResult);
        }
    }
}
