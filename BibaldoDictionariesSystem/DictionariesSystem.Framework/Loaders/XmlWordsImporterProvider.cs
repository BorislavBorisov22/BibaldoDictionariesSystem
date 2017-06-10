using Bytes2you.Validation;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;
using System;
using System.Linq;
using System.Xml;

namespace DictionariesSystem.Framework.Loaders
{
    public class XmlWordsImporterProvider : IWordsImporterProvider
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Dictionary> dictionariesRepository;
        private readonly IRepository<Word> wordsRepository;

        public XmlWordsImporterProvider(IRepository<Dictionary> dictionariesRepository, IRepository<Word> wordsRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(wordsRepository, "wordsRepostiory").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(dictionariesRepository, "dictionariesRepository").IsNull().Throw();

            this.wordsRepository = wordsRepository;
            this.unitOfWork = unitOfWork;
            this.dictionariesRepository = dictionariesRepository;
        }

        public void Import(string filePath, string dictionaryTitle)
        {
            Guard.WhenArgument(filePath, "filePath").IsNullOrEmpty().Throw();
            Guard.WhenArgument(dictionaryTitle, "dictionaryTitle").IsNullOrEmpty().Throw();

            var targetDictionary = this.dictionariesRepository.All(x => x.Title == dictionaryTitle).FirstOrDefault();
            Guard.WhenArgument(targetDictionary, "No dictionary with such title exists").IsNull().Throw();

            using (var xmlReader = XmlReader.Create(filePath))
            {
                Word word = this.ReadSingleWord(xmlReader, targetDictionary);
                while (word != null)
                {
                    this.wordsRepository.Add(word);

                    word = this.ReadSingleWord(xmlReader, targetDictionary);
                }
            }

            this.unitOfWork.SaveChanges();
        }

        private Word ReadSingleWord(XmlReader xmlReader, Dictionary targetDictionary)
        {
            bool isNameRead = false;
            bool isMeaningRead = false;
            bool isSpeechPartRead = false;

            Word word = new Word() { Dictionary = targetDictionary };
            Meaning meaning = new Meaning();

            while ((!isNameRead || !isMeaningRead || !isSpeechPartRead) && xmlReader.Read())
            {
                if (xmlReader.IsStartElement() && xmlReader.Name == "name")
                {
                    xmlReader.Read();
                    word.Name = xmlReader.Value;
                    isNameRead = true;
                }

                if (xmlReader.IsStartElement() && xmlReader.Name == "speechPart")
                {
                    xmlReader.Read();
                    word.SpeechPart = (SpeechPart)Enum.Parse(typeof(SpeechPart), xmlReader.Value);
                }

                if (xmlReader.IsStartElement() && xmlReader.Name == "meaning")
                {
                    xmlReader.Read();
                    meaning.Description = xmlReader.Value;
                    isMeaningRead = true;
                }
            }

            if (!isNameRead || !isMeaningRead || !isSpeechPartRead)
            {
                return null;
            }

            return word;
        }
    }
}