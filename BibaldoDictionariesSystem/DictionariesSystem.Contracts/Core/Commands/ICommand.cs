using System.Collections.Generic;

namespace DictionariesSystem.Contracts.Core.Commands
{
    public interface ICommand
    {
        string Execute(IList<string> parameters);
    }
}
