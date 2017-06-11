using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Create;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.Framework.Tests.Core.CommandTests.Create
{
    [TestFixture]
    public class AddWordToDictionaryTests
    {
        [Test]
        public void ConstructorShould_ThrowWhen_NullRepositoryIsPassed()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddWordToDictionaryCommand(null, unitOfWork.Object, userProvider.Object, dictionaryFactory.Object));
        }

        [Test]
        public void ConstructorShould_ThrowWhen_NullUnitOfWorkIsPassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddWordToDictionaryCommand(repository.Object, null, userProvider.Object, dictionaryFactory.Object));
        }

        [Test]
        public void ConstructorShould_ThrowWhen_NullUserProviderIsPassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.Throws<ArgumentNullException>(() => new AddWordToDictionaryCommand(repository.Object, unitOfWork.Object, null, dictionaryFactory.Object));
        }

        [Test]
        public void ConstructorShould_ThrowWhen_NullDictionaryFactoryIsPassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();

            Assert.Throws<ArgumentNullException>(() => new AddWordToDictionaryCommand(repository.Object, unitOfWork.Object, userProvider.Object, null));
        }

        [Test]
        public void ConstructorShould_NotThrow_WhenValidParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.DoesNotThrow(() => new AddWordToDictionaryCommand(repository.Object, unitOfWork.Object, userProvider.Object, dictionaryFactory.Object));
        }

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
