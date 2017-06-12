using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Create;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Users;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Create.CreateDictionaryCommandTests
{ 
    public class Execute_Should
    {
        [Test]
        public void Throw_WhenLessThanTwoParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();
            var command = new CreateDictionaryCommand(repository.Object, unitOfWork.Object,
                userProvider.Object, dictionaryFactory.Object);

            var parameters = new List<string>()
            {
                "wordName"
            };

            Assert.Throws<ArgumentException>(() => command.Execute(parameters));
        }

        [Test]
        public void Throw_WhenMoreThanTwoParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();
            var command = new CreateDictionaryCommand(repository.Object, unitOfWork.Object,
                userProvider.Object, dictionaryFactory.Object);

            var parameters = new List<string>()
            {
                "wordName",
                "dictionaryTitle",
                "speechPart"
            };

            Assert.Throws<ArgumentException>(() => command.Execute(parameters));
        }

        [Test]
        public void ReturnCorrectMessage_WhenMoreThanTwoParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            string authorName = "Whoever";
            string dictionaryTitle = "Vse Taq";
            string languageName = "English";

            User user = new User()
            {
                Username = authorName
            };
            userProvider.Setup(u => u.LoggedUser).Returns(user);

            var language = new Language() { Name = languageName };

            var dictionary = new Dictionary()
            {
                Author = authorName,
                Language = language,
                Title = dictionaryTitle
            };          

            dictionaryFactory.Setup(d => d.GetLanguage(languageName)).Returns(language);
            dictionaryFactory.Setup(d => d.GetDictionary(dictionaryTitle, authorName, 
                language, It.IsAny<DateTime>()))
                .Returns(dictionary);

            var command = new CreateDictionaryCommand(repository.Object, unitOfWork.Object,
                userProvider.Object, dictionaryFactory.Object);         

            var parameters = new List<string>()
            {
                dictionaryTitle,
                languageName
            };

            string expectedResult = $"A new dictionary with title {dictionaryTitle}, author {authorName} and language {languageName} was created.";
            string result = command.Execute(parameters);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
