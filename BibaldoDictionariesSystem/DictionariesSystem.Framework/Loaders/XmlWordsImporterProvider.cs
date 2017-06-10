using Bytes2you.Validation;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;
using System;
using System.Xml;

namespace DictionariesSystem.Framework.Loaders
{
    public class XmlWordsImporterProvider : IWordsImporterProvider
    {

        private readonly Dictionary targetDictionary;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Word> wordsRepository;

        public XmlWordsImporterProvider(IRepository<Word> wordsRepository, IUnitOfWork unitOfWork, Dictionary targetDictionary)
        {
            Guard.WhenArgument(wordsRepository, "wordsRepostiory").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.unitOfWork = unitOfWork;
            this.wordsRepository = wordsRepository;
        }

        public void Import(string filePath)
        {
            Guard.WhenArgument(filePath, "filePath").IsNullOrEmpty().Throw();

            using (var xmlReader = XmlReader.Create(filePath))
            {
                Word word = this.ReadSingleWord(xmlReader);
                while (word != null)
                {
                    this.wordsRepository.Add(word);

                    word = this.ReadSingleWord(xmlReader);
                }
            }

            this.unitOfWork.SaveChanges();
        }

        private Word ReadSingleWord(XmlReader xmlReader)
        {
            bool isNameRead = false;
            bool isMeaningRead = false;
            bool isSpeechPartRead = false;

            Word word = new Word() { Dictionary = this.targetDictionary };
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
