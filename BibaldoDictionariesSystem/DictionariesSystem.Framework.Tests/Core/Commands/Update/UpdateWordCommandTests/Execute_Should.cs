using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Update;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Update.UpdateWordCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenNoSuchDictionaryIsPresent()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>());

            string dictionaryTitle = "SomeTitle";
            string wordName = "SomeName";
            string newMeaning = "Some Meaning To Add";

            var parameters = new List<string>()
            {
                dictionaryTitle,
                wordName,
                newMeaning
            };

            var updateWordCommad = new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object);

            // act and assert
            Assert.Throws<ArgumentNullException>(() => updateWordCommad.Execute(parameters));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedWordNameIsNotPresentInTheDictionary()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            var dictionary = new Dictionary();
            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            string dictionaryTitle = "SomeTitle";
            string wordName = "SomeName";
            string newMeaning = "Some Meaning To Add";

            var parameters = new List<string>()
            {
                dictionaryTitle,
                wordName,
                newMeaning
            };

            var updateWordCommad = new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object);

            // act and assert
            Assert.Throws<ArgumentNullException>(() => updateWordCommad.Execute(parameters));
        }

        [Test]
        public void CallDictionariesFactoryGetMeaningMethodWithValidDescriptionParameter_WhenThereIsSuchWordAndDictionaryPresent()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            string dictionaryTitle = "SomeTitle";
            string wordName = "SomeName";
            string newMeaning = "Some Meaning To Add";

            var targetWord = new Word() { Name = wordName };

            var dictionary = new Dictionary();
            dictionary.Words.Add(targetWord);
            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var meaning = new Meaning() { Description = newMeaning };
            dictionariesFactory
                .Setup(x => x.GetMeaning(newMeaning))
                .Returns(meaning);

            var parameters = new List<string>()
            {
                dictionaryTitle,
                wordName,
                newMeaning
            };

            var updateWordCommad = new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object);

            // act
            updateWordCommad.Execute(parameters);

            // assert
            dictionariesFactory.Verify(x => x.GetMeaning(newMeaning), Times.Once);
        }

        [Test]
        public void AddTheNewMeaningToWord_WhenThereIsSuchWordAndDictionaryPresent()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            string dictionaryTitle = "SomeTitle";
            string wordName = "SomeName";
            string newMeaning = "Some Meaning To Add";

            var targetWord = new Word() { Name = wordName };

            var dictionary = new Dictionary();
            dictionary.Words.Add(targetWord);
            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var meaning = new Meaning() { Description = newMeaning };
            dictionariesFactory
                .Setup(x => x.GetMeaning(newMeaning))
                .Returns(meaning);

            var parameters = new List<string>()
            {
                dictionaryTitle,
                wordName,
                newMeaning
            };

            var updateWordCommad = new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object);

            // act
            updateWordCommad.Execute(parameters);

            // assert
            Assert.AreSame(targetWord.Meanings.First(), meaning);
        }

        [Test]
        public void CallUnitOfWorkSaveChangesMethod_WhenNewMeaningIsAddedToWordSuccessfully()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            string dictionaryTitle = "SomeTitle";
            string wordName = "SomeName";
            string newMeaning = "Some Meaning To Add";

            var targetWord = new Word() { Name = wordName };

            var dictionary = new Dictionary();
            dictionary.Words.Add(targetWord);
            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var meaning = new Meaning() { Description = newMeaning };
            dictionariesFactory
                .Setup(x => x.GetMeaning(newMeaning))
                .Returns(meaning);

            var parameters = new List<string>()
            {
                dictionaryTitle,
                wordName,
                newMeaning
            };

            var updateWordCommad = new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object);

            // act
            updateWordCommad.Execute(parameters);

            // assert
            unitOfWork.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnCorrectResultMessage_WhenNewMeaningIsAddedToWordSuccessfully()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionariesFactory = new Mock<IDictionariesFactory>();

            string dictionaryTitle = "SomeTitle";
            string wordName = "SomeName";
            string newMeaning = "Some Meaning To Add";

            var targetWord = new Word() { Name = wordName };

            var dictionary = new Dictionary();
            dictionary.Words.Add(targetWord);
            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var meaning = new Meaning() { Description = newMeaning };
            dictionariesFactory
                .Setup(x => x.GetMeaning(newMeaning))
                .Returns(meaning);

            var parameters = new List<string>()
            {
                dictionaryTitle,
                wordName,
                newMeaning
            };

            var updateWordCommad = new UpdateWordCommand(dictionariesRepository.Object, unitOfWork.Object, dictionariesFactory.Object);

            // act
            string resultMessage = updateWordCommad.Execute(parameters);

            // assert
            StringAssert.Contains(wordName, resultMessage);
            StringAssert.Contains("Added new meaning", resultMessage);
        }
    }
}