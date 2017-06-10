using DictionariesSystem.Models.Dictionaries;
using System;

namespace DictionariesSystem.Contracts.Core.Factories
{
    public interface IDictionariesFactory
    {
        Dictionary GetDictionary(string title, string author, Language language, DateTime dateTime);

        Word GetWord(string name, string SpeechPart, Word rootWord = null);

        Meaning GetMeaning(string description);

        Language GetLanguage(string name);
    }
}
