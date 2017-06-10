using System;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Core.Commands;
using Bytes2you.Validation;

namespace DictionariesSystem.Framework.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected abstract int ParametersCount { get; }

        public virtual string Execute(IList<string> parameters)
        {
            Guard.WhenArgument(parameters.Count, "ParametersCount").IsNotEqual(ParametersCount).Throw();
            return string.Empty;
        }
    }
}
