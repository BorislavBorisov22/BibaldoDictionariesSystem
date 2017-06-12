using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Delete;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Delete.DeleteDictionaryCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDictionaryWithPassedTitleDoesNotExist()
        {
            // arrange
            var dictionariesRepostiory = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            dictionariesRepostiory.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>());

            var deleteDictionaryCommand = new DeleteDictionaryCommand(dictionariesRepostiory.Object, unitOfWork.Object);
            string dictionaryTitle = "SomeTitle";

            var commandParameters = new List<string>() { dictionaryTitle };

            // act and assert
            Assert.Throws<ArgumentNullException>(() => deleteDictionaryCommand.Execute(commandParameters));
        }

        [Test]
        public void CallDictionariesRepositoryDeleteMethodWithTheCorrectDictionary_WhenPassedDictionaryTitleParameterIsValid()
        {
            // arrange
            var dictionariesRepostiory = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            string dictionaryTitle = "SomeTitle";
            var dictionary = new Dictionary() { Title = dictionaryTitle };

            dictionariesRepostiory.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var deleteDictionaryCommand = new DeleteDictionaryCommand(dictionariesRepostiory.Object, unitOfWork.Object);
            var commandParameters = new List<string>() { dictionaryTitle };

            // act
            deleteDictionaryCommand.Execute(commandParameters);

            // assert
            dictionariesRepostiory.Verify(x => x.Delete(It.Is<Dictionary>(d => d == dictionary)), Times.Once);
        }

        [Test]
        public void CallUnitOfWorksSaveChangesMethod_WhenDictionaryHasBeenSuccessfullyRemoved()
        {
            // arrange
            var dictionariesRepostiory = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            string dictionaryTitle = "SomeTitle";
            var dictionary = new Dictionary() { Title = dictionaryTitle };

            dictionariesRepostiory.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var deleteDictionaryCommand = new DeleteDictionaryCommand(dictionariesRepostiory.Object, unitOfWork.Object);
            var commandParameters = new List<string>() { dictionaryTitle };

            // act
            deleteDictionaryCommand.Execute(commandParameters);

            // assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnCorrectMessage_WhenDictionaryHasBeenSuccessfullyRemoved()
        {
            // arrange
            var dictionariesRepostiory = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            string dictionaryTitle = "SomeTitle";
            var dictionary = new Dictionary() { Title = dictionaryTitle };

            dictionariesRepostiory.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var deleteDictionaryCommand = new DeleteDictionaryCommand(dictionariesRepostiory.Object, unitOfWork.Object);
            var commandParameters = new List<string>() { dictionaryTitle };

            // act
            string resultMessage = deleteDictionaryCommand.Execute(commandParameters);

            // assert
            StringAssert.Contains("Deleted", resultMessage);
            StringAssert.Contains(dictionaryTitle, resultMessage);
        }
    }
}
