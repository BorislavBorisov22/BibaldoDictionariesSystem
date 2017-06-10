using DictionariesSystem.Contracts.Core.Commands;

namespace DictionariesSystem.Contracts.Core.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandName);
    }
}
