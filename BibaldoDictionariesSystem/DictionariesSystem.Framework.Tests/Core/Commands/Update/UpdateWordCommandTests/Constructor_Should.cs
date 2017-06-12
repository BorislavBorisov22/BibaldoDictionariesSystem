using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Update;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Update.UpdateWordCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedDictionariesRepositoryIsNull()
        {
            // arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UpdateWordCommand(null, unitOfWork.Object, dictionariesFactory.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedUnitOfWorkIsNull()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UpdateWordCommand(dictionariesRepository.Object, null, dictionariesFactory.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedDictionariesFactoryIsNull()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            // act and assert
            Assert.Throws<ArgumentNullException>
                (() => new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, null));
        }

        [Test]
        public void NotThrow_WhenPassedArgumentAreValid()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            // act and assert
            Assert.DoesNotThrow
                (() => new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object));
        }
    }
}
