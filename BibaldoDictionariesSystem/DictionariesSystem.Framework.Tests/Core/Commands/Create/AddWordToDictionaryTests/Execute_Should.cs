using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Create;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
    }
}
