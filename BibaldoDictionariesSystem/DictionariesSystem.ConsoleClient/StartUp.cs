using DictionariesSystem.ConsoleClient.Container;
using DictionariesSystem.Contracts.Core.Commands;
using Ninject;
using System.Collections.Generic;

namespace DictionariesSystem.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            var module = new DictionariesSystemModule();
            var kernel = new StandardKernel(module);
            //var engine = kernel.Get<IEngine>();
            //engine.Start();

            var command = kernel.Get<ICommand>(DictionariesSystemModule.GeneratePdfReportCommandName);
            string result = command.Execute(new List<string>());
            System.Console.WriteLine(result);

            //var loggingDbContext = new LogsDbContext();
            //loggingDbContext.ExceptionLogs.FirstOrDefault();

            //var dictionaryDbContext = new DictionariesDbContext();
            //dictionaryDbContext.Languages.FirstOrDefault();

            //var usersDbContext = new UsersDbContext();
            //usersDbContext.Users.FirstOrDefault();
        }
    }
}