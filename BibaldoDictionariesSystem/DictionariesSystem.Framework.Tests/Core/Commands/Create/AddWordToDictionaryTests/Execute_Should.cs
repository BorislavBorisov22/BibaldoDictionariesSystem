using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Create;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Create.AddWordToDictionaryTests
{
    [TestFixture]
    public class Execute_Should
    {
        [Test]
        public void ExcecuteShould_Throw_LessThanFourParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();
            var command = new AddWordToDictionaryCommand(repository.Object, unitOfWork.Object,
                userProvider.Object, dictionaryFactory.Object);

            var parameters = new List<string>()
            {
                "wordName",
                "dictionaryTitle",
                "speechPart"
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => command.Execute(parameters));
        }

        [Test]
        public void ExcecuteShould_ReturnCorrect_WhenValidParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();

            var language = new Language()
            {
                Name = "English"
            };

            var dictionary = new Dictionary
            {
                Title = "dictionaryTitle",
                Author = "someAuthor",
                Language = language
            };

            var dictionaries = new List<Dictionary>()
            {
                dictionary
            };

            repository.Setup(x => x.All(It.IsAny<Expression<Func<Dictionary, bool>>>())).Returns(dictionaries);
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();
            dictionaryFactory.Setup(d => d.GetWord("cat", "speechPart", null)).Returns(new Word() { Name = "Cat", SpeechPart = SpeechPart.NotSpecified });
            dictionaryFactory.Setup(d => d.GetMeaning("animal home")).Returns(new Meaning()
            {
                Description = "animal home"
            });

            var command = new AddWordToDictionaryCommand(repository.Object, unitOfWork.Object,
                userProvider.Object, dictionaryFactory.Object);

            string wordName = "cat";
            string dictionaryTitle = "dictionaryTitle";
            string speechPart = "speechPart";
            string desc1 = "animal";
            string desc2 = "home";
            string wordDescription = desc1 + ' ' + desc2;

            var parameters = new List<string>()
            {
                wordName,
                dictionaryTitle,
                speechPart,
                desc1,
                desc2
            };

            string expectedResult = $"A new word: {wordName} was added into dictionary: {dictionaryTitle}\n{wordName} means {wordDescription}";
            string result = command.Execute(parameters);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
