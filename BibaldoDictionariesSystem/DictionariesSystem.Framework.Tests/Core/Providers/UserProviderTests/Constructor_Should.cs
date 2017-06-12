using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Providers
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumenNullException_WhenPassedUserRepositoryIsNull()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>
                (() => new UserProvider(null, new Mock<IUnitOfWork>().Object, new Mock<IUserFactory>().Object));
        }

        [Test]
        public void ThrowArgumenNullException_WhenPassedUnitOfWorkIsNull()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>
                (() => new UserProvider(new Mock<IRepository<User>>().Object, null, new Mock<IUserFactory>().Object));
        }


        [Test]
        public void ThrowArgumenNullException_WhenPassedUserFactoryIsNull()
        {
            // arrange & act & assert
            Assert.Throws<ArgumentNullException>
                 (() => new UserProvider(new Mock<IRepository<User>>().Object, new Mock<IUnitOfWork>().Object, null));
        }
        
        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            // arrange & act & assert
            Assert.DoesNotThrow
                (() => new UserProvider(new Mock<IRepository<User>>().Object, new Mock<IUnitOfWork>().Object, new Mock<IUserFactory>().Object));
        }
    }
}
