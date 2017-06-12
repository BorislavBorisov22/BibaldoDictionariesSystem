using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Models.Dictionaries;
using Moq;
using NUnit.Framework;
using System;

namespace DictionariesSystem.Framework.Tests.Core.Commands.Read.ListDictionaryCommandTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void Throw_WhenNullRepositoryIsPassed()
        {
            Assert.Throws<ArgumentNullException>(() => new ListDictionaryCommand(null));
        }

        [Test]
        public void NotThrow_WhenRepositoryIsPassed()
        {
            var dictionaries = new Mock<IRepository<Dictionary>>();

            Assert.DoesNotThrow(() => new ListDictionaryCommand(dictionaries.Object));
        }
    }
}
