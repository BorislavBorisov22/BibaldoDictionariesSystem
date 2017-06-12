using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Delete;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Delete.DeleteDictionaryCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedDictionariesRepositoryIsNull()
        {
            // arrange and act and assert
            Assert.Throws<ArgumentNullException>
                (() => new DeleteDictionaryCommand(null, new Mock<IUnitOfWork>().Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUnitOfWorkIsNull()
        {
            // arrange and act and assert
            Assert.Throws<ArgumentNullException>
                (() => new DeleteDictionaryCommand(new Mock<IRepository<Dictionary>>().Object,null));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            Assert.DoesNotThrow
                (() => new DeleteDictionaryCommand(
                    new Mock<IRepository<Dictionary>>().Object,
                    new Mock<IUnitOfWork>().Object));
        }
    }
}
