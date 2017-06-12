using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Read.ListWordInformationCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenDictionaryWithTheProvidedTitleIsNotFound()
        {
            // arrange
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();

            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>());

            string dictionaryName = "SomeDictName";
            string wordName = "SomeWordName";

            var listWordInformationCommand = new ListWordInformationCommand(dictionariesRepository.Object);
            var arguments = new List<string>() { dictionaryName, wordName };

            // act and assert
            Assert.Throws<ArgumentNullException>(() => listWordInformationCommand.Execute(arguments));
        }

        [Test]
        public void ThrowArgumentNullException_WhenTargetDictionaryDoesNotContainPassedWordName()
        {
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();

            string dictionaryName = "SomeDictName";
            string wordName = "SomeWordName";
            var dictionary = new Dictionary()
            {
                Title = dictionaryName
            };

            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var listWordInformationCommand = new ListWordInformationCommand(dictionariesRepository.Object);
            var arguments = new List<string>() { dictionaryName, wordName };

            // act and assert
            Assert.Throws<ArgumentNullException>(() => listWordInformationCommand.Execute(arguments));
        }

        [Test]
        public void ReturnCorrectWordInformationString_WhenWordExistsInDictionaryButDoesntHaveARoot()
        {
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();

            string dictionaryName = "SomeDictName";
            string wordName = "SomeWordName";

            var word = new Word()
            {
                Name = wordName,
                SpeechPart = SpeechPart.Adverb,
                Meanings = new List<Meaning>()
            {
                new Meaning() { Description = "SomeDescription" } }

            };
            var dictionary = new Dictionary() { Title = dictionaryName };
            dictionary.Words.Add(word);

            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var listWordInformationCommand = new ListWordInformationCommand(dictionariesRepository.Object);
            var arguments = new List<string>() { dictionaryName, wordName };

            // act
            string resultMessage = listWordInformationCommand.Execute(arguments);

            // assert
            StringAssert.Contains($"{word.Name} - {word.SpeechPart}", resultMessage);
            StringAssert.Contains(word.Meanings.First().Description, resultMessage);
        }

        [Test]
        public void ReturnCorrectWordInformationString_WhenWordExistingHasARootWord()
        {
            var dictionariesRepository = new Mock<IRepository<Dictionary>>();

            string dictionaryName = "SomeDictName";
            string wordName = "SomeWordName";

            var root = new Word()
            {
                Name = "SomeRootName"
            };

            var word = new Word()
            {
                Name = wordName,
                SpeechPart = SpeechPart.Adverb,
                RootWord = root,
                Meanings = new List<Meaning>()
            {
                new Meaning() { Description = "SomeDescription" } }

            };

            var dictionary = new Dictionary() { Title = dictionaryName };
            dictionary.Words.Add(word);

            dictionariesRepository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>()))
                .Returns(new List<Dictionary>() { dictionary });

            var listWordInformationCommand = new ListWordInformationCommand(dictionariesRepository.Object);
            var arguments = new List<string>() { dictionaryName, wordName };

            // act
            string resultMessage = listWordInformationCommand.Execute(arguments);

            // assert
            StringAssert.Contains(root.Name, resultMessage);
        }
    }
}
