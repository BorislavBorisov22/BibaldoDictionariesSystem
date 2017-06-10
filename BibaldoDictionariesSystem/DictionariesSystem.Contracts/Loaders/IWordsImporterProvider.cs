namespace DictionariesSystem.Contracts.Loaders
{
    public interface IWordsImporterProvider
    {
        void Import(string filePath);
    }
}
