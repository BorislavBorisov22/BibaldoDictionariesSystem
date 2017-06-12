using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Read.ListDictionaryCommandTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void Throw_WhenPassedParamsArentTheRightCount()
        {
            var dictionaries = new Mock<IRepository<Dictionary>>();
            var command = new ListDictionaryCommand(dictionaries.Object);

            var parameters = new List<string>();

            Assert.Throws<ArgumentException>(() => command.Execute(parameters));
        }

        [Test]
        public void ReturnCorrectMessage_WhenPassedParametersAreValid()
        {
            var dictionaries = new Mock<IRepository<Dictionary>>();
            var command = new ListDictionaryCommand(dictionaries.Object);

            string dictionaryName = "dictionaryName";
            string dictTitle = "dictTitle";
            var language = new Language()
            {
                Name = "English"
            };
            var dictionary = new Dictionary()
            {
                Title = dictTitle,
                Language = language,
                Author = "somebody"
            };

            var createdDictionaries = new List<Dictionary>() { dictionary };
            var parameters = new List<string>()
            {
                dictionaryName
            };

            dictionaries.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>())).Returns(createdDictionaries);
            string result = command.Execute(parameters);

            StringAssert.Contains($"{dictTitle}", result);
            StringAssert.Contains($"{language.Name}", result);
            StringAssert.Contains($"somebody", result);
        }

        [Test]
        public void ReturnCorrectMessage_WhenDictionaryHasWords()
        {
            var dictionaries = new Mock<IRepository<Dictionary>>();
            var command = new ListDictionaryCommand(dictionaries.Object);

            string dictionaryName = "dictionaryName";
            string dictTitle = "dictTitle";
            var language = new Language()
            {
                Name = "English"
            };

            var word = new Word()
            {
                Name = "Cat",
                SpeechPart = SpeechPart.NotSpecified
            };


            var dictionary = new Dictionary()
            {
                Title = dictTitle,
                Language = language,
                Author = "somebody"
            };

            dictionary.Words.Add(word);

            var createdDictionaries = new List<Dictionary>() { dictionary };
            var parameters = new List<string>()
            {
                dictionaryName
            };

            dictionaries.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>())).Returns(createdDictionaries);
            string result = command.Execute(parameters);

            StringAssert.Contains($"Cat", result);
        }
    }
}
