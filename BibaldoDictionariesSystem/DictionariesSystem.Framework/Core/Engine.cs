using System;
using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Contracts.Core.Providers;
using Bytes2you.Validation;
using DictionariesSystem.Framework.Core.Exceptions;

namespace DictionariesSystem.Framework.Core
{
    public class Engine : IEngine
    {
        private const string TerminateCommand = "Exit";
        private const string InvalidCommandMessage = "Invalid command!";

        private readonly ICommandProcessor commandProcessor;
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly ILogger logger;
        
        public Engine(ICommandProcessor commandProcessor, IWriter writer, IReader reader, ILogger logger)
        {
            Guard.WhenArgument(commandProcessor, "commandProcessor").IsNull().Throw();
            Guard.WhenArgument(writer, "writer").IsNull().Throw();
            Guard.WhenArgument(reader, "reader").IsNull().Throw();
            Guard.WhenArgument(logger, "logger").IsNull().Throw();
            
            this.commandProcessor = commandProcessor;
            this.writer = writer;
            this.reader = reader;
            this.logger = logger;
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    string commandAsText = this.reader.ReadLine();
                    if (commandAsText == TerminateCommand)
                    {
                        break;
                    }

                    var commandMessage = this.commandProcessor.ProcessCommand(commandAsText);
                    this.writer.WriteLine(commandMessage);
                }
                catch (Exception ex)
                {
                    this.logger.Log(ex.Message);
                    this.writer.WriteLine(InvalidCommandMessage);
                }
            }
        }
    }
}