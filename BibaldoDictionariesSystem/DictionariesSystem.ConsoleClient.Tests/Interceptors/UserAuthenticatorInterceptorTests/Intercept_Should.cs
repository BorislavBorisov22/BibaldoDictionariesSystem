using DictionariesSystem.ConsoleClient.Interceptors;
using DictionariesSystem.Contracts.Core.Providers;
using Moq;
using Ninject.Extensions.Interception;
using NUnit.Framework;

namespace DictionariesSystem.ConsoleClient.Tests.Interceptors.UserAuthenticatorInterceptorTests
{
    [TestFixture]
    public class Intercept_Should
    {
        [Test]
        public void CallInvocationProceedMethod_WhenUserIsLogged()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            userProvider.Setup(x => x.IsLogged).Returns(true);

            var interceptor = new UserAuthenticatorInterceptor(userProvider.Object);
            
            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed());

            // act
            interceptor.Intercept(invocationMock.Object);

            // assert
            invocationMock.Verify(x => x.Proceed(), Times.Once);
        }

        [Test]
        public void NotCallInvocationProceedMethod_WhenUserIsNotLogged()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            userProvider.Setup(x => x.IsLogged).Returns(false);

            var interceptor = new UserAuthenticatorInterceptor(userProvider.Object);
            
            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed());
            invocationMock.Setup(x => x.Request.Target).Returns(new object());
            invocationMock.SetupSet(x => x.ReturnValue = It.IsAny<string>());

            // act
            interceptor.Intercept(invocationMock.Object);

            // assert
            invocationMock.Verify(x => x.Proceed(), Times.Never);
        }

        [Test]
        public void SetTheReturnValueOfTheInvocation_WhenUserIsNotLogged()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();
            userProvider.Setup(x => x.IsLogged).Returns(false);

            var interceptor = new UserAuthenticatorInterceptor(userProvider.Object);

            var invocationMock = new Mock<IInvocation>();
            invocationMock.Setup(x => x.Proceed());
            invocationMock.Setup(x => x.Request.Target).Returns(new object());
            invocationMock.SetupSet(x => x.ReturnValue = It.IsAny<string>());

            // act
            interceptor.Intercept(invocationMock.Object);

            // assert
            invocationMock.VerifySet(x => x.ReturnValue = It.IsAny<string>(), Times.Once);
        }
    }
}
