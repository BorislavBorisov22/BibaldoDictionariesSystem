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

namespace DictionariesSystem.Framework.Tests.Core.Commands.Create.CreateDictionaryCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ConstructorShould_ThrowWhen_NullRepositoryIsPassed()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.Throws<ArgumentNullException>(() => new CreateDictionaryCommand(null, unitOfWork.Object, userProvider.Object, dictionaryFactory.Object));
        }

        [Test]
        public void ConstructorShould_ThrowWhen_NullUnitOfWorkIsPassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.Throws<ArgumentNullException>(() => new CreateDictionaryCommand(repository.Object, null, userProvider.Object, dictionaryFactory.Object));
        }

        [Test]
        public void ConstructorShould_ThrowWhen_NullUserProviderIsPassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.Throws<ArgumentNullException>(() => new CreateDictionaryCommand(repository.Object, unitOfWork.Object, null, dictionaryFactory.Object));
        }

        [Test]
        public void Throw_WhenNullDictionaryFactoryIsPassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();

            Assert.Throws<ArgumentNullException>(() => new CreateDictionaryCommand(repository.Object, unitOfWork.Object, userProvider.Object, null));
        }

        [Test]
        public void NotThrow_WhenValidParametersArePassed()
        {
            var repository = new Mock<IRepository<Dictionary>>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var userProvider = new Mock<IUserProvider>();
            var dictionaryFactory = new Mock<IDictionariesFactory>();

            Assert.DoesNotThrow(() => new CreateDictionaryCommand(repository.Object, unitOfWork.Object, userProvider.Object, dictionaryFactory.Object));
        }
    }
}
