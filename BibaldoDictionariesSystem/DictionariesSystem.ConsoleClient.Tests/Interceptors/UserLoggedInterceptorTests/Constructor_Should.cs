using DictionariesSystem.ConsoleClient.Interceptors;
using DictionariesSystem.Contracts.Core.Providers;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.UserLoggedInterceptor
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedLoggerIsNull()
        {
            // arrange and act and assert
            Assert.Throws<ArgumentNullException>(() => new UserLoggerInterceptor(null));
        }

        [Test]
        public void NotThrow_WhenPassedLoggerArgumentIsValid()
        {
            // arrange and act and assert
            Assert.DoesNotThrow(() => new UserLoggerInterceptor(new Mock<ILogger>().Object));
        }
    }
}
