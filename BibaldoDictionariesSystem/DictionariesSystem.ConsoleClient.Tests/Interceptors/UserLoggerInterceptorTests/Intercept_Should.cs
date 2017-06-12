using DictionariesSystem.ConsoleClient.Interceptors;
using DictionariesSystem.ConsoleClient.Tests.Interceptors.UserLoggedInterceptorTests.Fakes;
using DictionariesSystem.Contracts.Core.Providers;
using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.UserLoggedInterceptor
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void CallLoggersLogMethodWithTheInterceptedClassName_WhenMethodCalled()
        {
            // arrange
            var invocation = new Mock<IInvocation>();
            var logger = new Mock<ILogger>();

            var fakeCommand = new FakeCommand();
            invocation.Setup(x => x.Request.Target).Returns(fakeCommand);

            var userLoggerInterceptor = new UserLoggerInterceptor(logger.Object);

            // act
            userLoggerInterceptor.Intercept(invocation.Object);

            // assert
            logger.Verify(x => x.Log("Fake"), Times.Once);
        }

        [Test]
        public void CallInvocationsProceedMethod_WhenMethodIsCalled()
        {
            // arrange
            var invocation = new Mock<IInvocation>();
            var logger = new Mock<ILogger>();

            var fakeCommand = new FakeCommand();
            invocation.Setup(x => x.Request.Target).Returns(fakeCommand);

            var userLoggerInterceptor = new UserLoggerInterceptor(logger.Object);

            // act
            userLoggerInterceptor.Intercept(invocation.Object);

            // assert
            invocation.Verify(x => x.Proceed(), Times.Once);
        }
    }
}