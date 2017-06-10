using System;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Core.Commands;
using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Factories;
using System.Collections.Generic;
using System.Linq;

namespace DictionariesSystem.Framework.Core.Providers
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandFactory commandFactory;

        public CommandProcessor(ICommandFactory commandFactory)
        {
            Guard.WhenArgument(commandFactory, "commandFactory").IsNull().Throw();

            this.commandFactory = commandFactory;
        }

        public string ProcessCommand(string commandAsText)
        {
            var commandParameters = commandAsText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            string commandName = commandParameters[0];
            commandParameters.RemoveAt(0);

            var command = this.commandFactory.CreateCommand(commandName);
            return command.Execute(commandParameters);
        }
    }
}
