namespace DictionariesSystem.ConsoleClient.Configuration
{
    public interface IConfigurationProvider
    {
        bool IsTestEnvironment();
    }
}
