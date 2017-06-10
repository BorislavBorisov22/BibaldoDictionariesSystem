using System;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Contracts.Data;
using Bytes2you.Validation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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
            Guard.WhenArgument(filePath, "filePath").IsNullOrEmpty().Throw();
            Guard.WhenArgument(dictionaryTitle, "dictionaryTitle").IsNullOrEmpty().Throw();

            var targetDictionary = this.dictionariesRepository
                .All(x => x.Title == dictionaryTitle)
                .FirstOrDefault();

            Guard.WhenArgument(targetDictionary, "No Dictionary with such title was found!").IsNull().Throw();

            var words = JsonConvert.DeserializeObject<IEnumerable<Word>>(filePath);

            foreach (var word in words)
            {
                word.Dictionary = targetDictionary;
                this.wordsRepository.Add(word);
            }

            this.unitOfWork.SaveChanges();
        }
    }
}
