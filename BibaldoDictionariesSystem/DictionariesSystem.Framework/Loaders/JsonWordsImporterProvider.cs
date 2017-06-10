using System;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Contracts.Data;
using Bytes2you.Validation;

namespace DictionariesSystem.Framework.Loaders
{
    public class JsonWordsImporterProvider : IWordsImporterProvider
    {
        private readonly IRepository<Dictionary> dictionariesRepository;
        private readonly IRepository<Word> wordsRepository;
        private readonly IUnitOfWork unitOfWork;

        public JsonWordsImporterProvider(IRepository<Dictionary> dictionariesRepository, IRepository<Word> wordsRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(dictionariesRepository, "dictionariesRepository").IsNull().Throw();
            Guard.WhenArgument(wordsRepository, "wordsRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.dictionariesRepository = dictionariesRepository;
            this.wordsRepository = wordsRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Import(string filePath, string dictionaryTitle)
        {
            throw new NotImplementedException();
        }
    }
}
