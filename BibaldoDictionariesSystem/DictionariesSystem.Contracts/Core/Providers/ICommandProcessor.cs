namespace DictionariesSystem.Contracts.Core.Providers
{
    public interface ICommandProcessor
    {
        string ProcessCommand(string commandAsText);
    }
}
