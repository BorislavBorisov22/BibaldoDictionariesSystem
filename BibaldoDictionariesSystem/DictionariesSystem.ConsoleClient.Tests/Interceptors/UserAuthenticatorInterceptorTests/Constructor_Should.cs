using NUnit.Framework;
using Moq;
using DictionariesSystem.Contracts.Core.Providers;
using System;
using DictionariesSystem.ConsoleClient.Interceptors;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.UserAuthenticatorInterceptorTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserProviderIsNull()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>(() => new UserAuthenticatorInterceptor(null));
        }

        [Test]
        public void NotThrow_WhenUserProviderIsCorrect()
        {
            // arrange & act & assert
            Assert.DoesNotThrow(() => new UserAuthenticatorInterceptor(new Mock<IUserProvider>().Object));
        }
    }
}
