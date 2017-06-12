using DictionariesSystem.ConsoleClient.Interceptors;
using DictionariesSystem.Framework.Core.Exceptions;
using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;
using System;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.CommandProcessorInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void CallInvocationProceedMethod()
        {
            // arrange
            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed());

            var interceptor = new CommandProcessorInterceptor();

            // act
            interceptor.Intercept(invocationMock.Object);

            // assert
            invocationMock.Verify(x => x.Proceed(), Times.Once);
        }

        [Test]
        public void NotThrow_WhenInvocationProceedMethodDoesNotThrow()
        {
            // arrange
            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed());

            var interceptor = new CommandProcessorInterceptor();

            // act & assert
            Assert.DoesNotThrow(() => interceptor.Intercept(invocationMock.Object));
        }

        [Test]
        public void RethrowUserAuthenticationException_WhenUserAuthenticationExceptionIsThrown()
        {
            // arrange
            string exceptionMessage = "message";

            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed()).Throws(new UserAuthenticationException(exceptionMessage));

            var interceptor = new CommandProcessorInterceptor();

            // act & assert
            Assert.Throws<UserAuthenticationException>(() => interceptor.Intercept(invocationMock.Object));
        }

        [Test]
        public void ThrowInvalidCommandException_WhenGenericExceptionIsThrown()
        {
            // arrange
            string exceptionMessage = "message";

            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed()).Throws(new Exception(exceptionMessage));

            var interceptor = new CommandProcessorInterceptor();

            // act & assert
            Assert.Throws<InvalidCommandException>(() => interceptor.Intercept(invocationMock.Object));
        }
    }
}
