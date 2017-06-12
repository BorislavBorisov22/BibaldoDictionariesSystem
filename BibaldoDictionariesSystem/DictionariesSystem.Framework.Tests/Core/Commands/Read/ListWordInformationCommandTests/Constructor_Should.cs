using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Read.ListWordInformationCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedDictionariesRepositoryIsNull()
        {
            // arrange
            var userProvider = new Mock<IUserProvider>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new ListWordInformationCommand(null));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentsAreValid()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
          
            // act and assert
            Assert.DoesNotThrow
                (() => new ListWordInformationCommand(dictionariesRepository.Object));
        }
    }
}