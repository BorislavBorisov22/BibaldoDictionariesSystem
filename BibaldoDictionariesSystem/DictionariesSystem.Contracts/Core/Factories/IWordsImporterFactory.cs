using DictionariesSystem.Contracts.Loaders;

namespace DictionariesSystem.Contracts.Core.Factories
{
    public interface IWordsImporterFactory
    {
        IWordsImporterProvider GetImporter(string name);
    }
}
