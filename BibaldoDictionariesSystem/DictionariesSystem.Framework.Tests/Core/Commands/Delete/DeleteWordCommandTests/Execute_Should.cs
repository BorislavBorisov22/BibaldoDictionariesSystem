using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Delete;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Delete.DeleteWordCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedDictionaryNameIsNotValid()
        {
            // arrange
            var dictionaryRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var dictionaries = new List<Dictionary>();
            dictionaryRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(dictionaries);

            var deleteWordCommand = new DeleteWordCommand(dictionaryRepository.Object, unitOfWork.Object);

            var commandParameters = new List<string>() { "SomeDictionary", "SomeWord" };

            // act and assert
            Assert.Throws<ArgumentNullException>(() => deleteWordCommand.Execute(commandParameters));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedWordIsNotPresentInProvidedDictionary()
        {
            // arrange
            var dictionaryRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var dictionary = new Dictionary() { Words = new List<Word>() };

            var dictionaries = new List<Dictionary>();
            dictionaries.Add(dictionary);

            dictionaryRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(dictionaries);

            var deleteWordCommand = new DeleteWordCommand(dictionaryRepository.Object, unitOfWork.Object);

            var commandParameters = new List<string>() { "SomeDictionary", "SomeWord" };

            // act and assert
            Assert.Throws<ArgumentNullException>(() => deleteWordCommand.Execute(commandParameters));
        }

        [Test]
        public void RemoveWordFromDictionary_WhenSuchWordsExistsInPassedDictionary()
        {
            // arrange
            var dictionaryRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var word = new Word() { Name = "SomeName" };
            var dictionary = new Dictionary() { Words = new List<Word>() { word } };

            var dictionaries = new List<Dictionary>();
            dictionaries.Add(dictionary);

            dictionaryRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(dictionaries);

            var deleteWordCommand = new DeleteWordCommand(dictionaryRepository.Object, unitOfWork.Object);

            var commandParameters = new List<string>() { "SomeDictionary", word.Name };

            // act
            deleteWordCommand.Execute(commandParameters);

            // assert
            CollectionAssert.DoesNotContain(dictionary.Words, word);
        }

        [Test]
        public void CallUnitOfWorkSaveChangesMethod_WhenWordHasBeenSuccessfullyRemovedFromDictionary()
        {
            // arrange
            var dictionaryRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var word = new Word() { Name = "SomeName" };
            var dictionary = new Dictionary() { Words = new List<Word>() { word } };

            var dictionaries = new List<Dictionary>();
            dictionaries.Add(dictionary);

            dictionaryRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(dictionaries);

            var deleteWordCommand = new DeleteWordCommand(dictionaryRepository.Object, unitOfWork.Object);

            var commandParameters = new List<string>() { "SomeDictionary", word.Name };

            // act
            deleteWordCommand.Execute(commandParameters);

            // assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnCorrectMessage_WhenWordsHasBeenDeletedSuccessfullyFromDictionary()
        {
            // arrange
            var dictionaryRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var word = new Word() { Name = "SomeName" };
            var dictionary = new Dictionary() { Title = "SomeDictionary", Words = new List<Word>() { word } };

            var dictionaries = new List<Dictionary>();
            dictionaries.Add(dictionary);

            dictionaryRepository
                .Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(dictionaries);

            var deleteWordCommand = new DeleteWordCommand(dictionaryRepository.Object, unitOfWork.Object);
            var commandParameters = new List<string>() { dictionary.Title, word.Name };

            // act
            string resultMessage = deleteWordCommand.Execute(commandParameters);

            // assert
            StringAssert.Contains(dictionary.Title, resultMessage);
            StringAssert.Contains(word.Name, resultMessage);
        }
    }
}
