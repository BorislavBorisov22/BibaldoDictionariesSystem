using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Framework.Loaders.ConvertModels;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace DictionariesSystem.Framework.Loaders
{
    public class JsonWordsImporterProvider : IWordsImporterProvider
    {
        private readonly IRepository<Dictionary> dictionariesRepository;
        private readonly IRepository<Word> wordsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDictionariesFactory dictionariesFactory;

        public JsonWordsImporterProvider(IRepository<Dictionary> dictionariesRepository, IRepository<Word> wordsRepository, IUnitOfWork unitOfWork, IDictionariesFactory dictionariesFactory)
        {
            Guard.WhenArgument(dictionariesRepository, "dictionariesRepository").IsNull().Throw();
            Guard.WhenArgument(wordsRepository, "wordsRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(dictionariesFactory, "dictionariesFactory").IsNull().Throw();

            this.dictionariesRepository = dictionariesRepository;
            this.wordsRepository = wordsRepository;
            this.unitOfWork = unitOfWork;
            this.dictionariesFactory = dictionariesFactory;
        }

        public void Import(string filePath, string dictionaryTitle)
        {
            Guard.WhenArgument(filePath, "filePath").IsNullOrEmpty().Throw();
            Guard.WhenArgument(dictionaryTitle, "dictionaryTitle").IsNullOrEmpty().Throw();

            var targetDictionary = this.dictionariesRepository
                .All(x => x.Title == dictionaryTitle)
                .FirstOrDefault();

            Guard.WhenArgument(targetDictionary, "No Dictionary with such title was found!").IsNull().Throw();

            string jsonContent = File.ReadAllText(filePath);

            var wordsCollectionModel = JsonConvert.DeserializeObject<JsonWordsCollectionModel>(jsonContent);

            foreach (var wordModel in wordsCollectionModel.Words)
            {
                var currentWordMeaning = dictionariesFactory.GetMeaning(wordModel.Description);

                SpeechPart wordSpeechPart;
                Enum.TryParse(wordModel.SpeechPart, out wordSpeechPart);
                var currentWord = dictionariesFactory.GetWord(wordModel.Name, wordModel.Description);
                currentWord.Meanings.Add(currentWordMeaning);
                currentWord.Dictionary = targetDictionary;

                this.wordsRepository.Add(currentWord);
            }

            this.unitOfWork.SaveChanges();
        }
    }
}