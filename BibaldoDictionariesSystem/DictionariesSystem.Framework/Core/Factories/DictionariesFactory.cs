using System;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Dictionaries.Enums;

namespace DictionariesSystem.Framework.Core.Factories
{
    public class DictionariesFactory : IDictionariesFactory
    {
        public Dictionary GetDictionary(string title, string author, Language language, DateTime dateTime)
        {
            return new Dictionary { Title = title, Author = author, Language = language, CreatedOn = dateTime };
        }

        public Language GetLanguage(string name)
        {
            return new Language() { Name = name};
        }

        public Meaning GetMeaning(string description)
        {
            return new Meaning() { Description = description };
        }

        public Word GetWord(string name, string speechPartString, Word rootWord = null)
        {
            SpeechPart speechPart = SpeechPart.NotSpecified;
            Enum.TryParse<SpeechPart>(speechPartString, out speechPart);

            return new Word() { Name = name, SpeechPart = speechPart, RootWord = rootWord };
        }
    }
}
