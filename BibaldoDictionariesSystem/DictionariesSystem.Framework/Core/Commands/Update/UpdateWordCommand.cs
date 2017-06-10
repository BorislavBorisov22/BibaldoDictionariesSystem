using System;
using DictionariesSystem.Contracts.Core.Commands;

namespace DictionariesSystem.Framework.Core.Commands.Update
{
    public class UpdateWordCommand : BaseCommand, ICommand
    {
        protected override int ParametersCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
