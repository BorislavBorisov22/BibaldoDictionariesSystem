using DictionariesSystem.ConsoleClient.Container;
using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Data.Dictionaries;
using DictionariesSystem.Models.Dictionaries;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DictionariesSystem.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            //var dictionaryDbContext = new DictionariesDbContext();
            //dictionaryDbContext.Dictionaries.Add(new Dictionary() { Title = "Roskata", Author = "Pak Roskata", Language = new Language() { Name = "Mandarin" }, CreatedOn = DateTime.Now });
            //dictionaryDbContext.SaveChanges();

            var module = new DictionariesSystemModule();
            var kernel = new StandardKernel(module);
            var engine = kernel.Get<IEngine>();
            engine.Start();

            //var command = kernel.Get<ICommand>(DictionariesSystemModule.GeneratePdfReportCommandName);
            //string result = command.Execute(new List<string>());
            //Console.WriteLine(result);

            //var loggingDbContext = new LogsDbContext();
            //loggingDbContext.ExceptionLogs.FirstOrDefault();
            
            //var usersDbContext = new UsersDbContext();
            //usersDbContext.Users.FirstOrDefault();
        }
    }
}