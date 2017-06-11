using System;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Core.Commands;

namespace DictionariesSystem.Framework.Core.Commands.Common
{
    public class ClearCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "";
        private const int NumberOfParameters = 0;

        protected override int ParametersCount
        {
            get
            {
                return NumberOfParameters;
            }
        }

        public override string Execute(IList<string> parameters)
        {
            var result = base.Execute(parameters);

            Console.Clear();

            return result;
        }
    }
}
