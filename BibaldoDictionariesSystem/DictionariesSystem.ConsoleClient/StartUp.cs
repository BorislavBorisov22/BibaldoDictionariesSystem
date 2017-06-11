using DictionariesSystem.ConsoleClient.Container;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Data.Common;
using DictionariesSystem.Data.Dictionaries;
using DictionariesSystem.Framework.Core.Factories;
using DictionariesSystem.Framework.Loaders;
using DictionariesSystem.Framework.Loaders.ConvertModels;
using DictionariesSystem.Models.Dictionaries;
using Newtonsoft.Json;
using Ninject;
using System.Collections.Generic;
using System.IO;

namespace DictionariesSystem.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            //var module = new DictionariesSystemModule();
            //var kernel = new StandardKernel(module);
            ////var engine = kernel.Get<IEngine>();
            ////engine.Start();

            //var command = kernel.Get<ICommand>(DictionariesSystemModule.GeneratePdfReportCommandName);
            //string result = command.Execute(new List<string>());
            //System.Console.WriteLine(result);

            //var loggingDbContext = new LogsDbContext();
            //loggingDbContext.ExceptionLogs.FirstOrDefault();

            //var dictionaryDbContext = new DictionariesDbContext();
            //dictionaryDbContext.Languages.FirstOrDefault();

            var dictionariesDbContext = new DictionariesDbContext();
            var wordsRepository = new Repository<Word>(dictionariesDbContext);
            var dicitonariesRepostiory = new Repository<Dictionary>(dictionariesDbContext);
            var unitOfWork = new UnitOfWork(dictionariesDbContext);
            var dictionariesFactory = new DictionariesFactory();

            var jsonImporter = new JsonWordsImporterProvider(dicitonariesRepostiory, wordsRepository, unitOfWork, dictionariesFactory);

            //string json = File.ReadAllText(@"C:\Users\bobi\Desktop\DatabasesTeamwork\BibaldoDictionariesSystem\BibaldoDictionariesSystem\words.json");
            //JsonWordsCollectionModel obj = JsonConvert.DeserializeObject<JsonWordsCollectionModel>(json);

            jsonImporter.Import(@"C:\Users\bobi\Desktop\DatabasesTeamwork\BibaldoDictionariesSystem\BibaldoDictionariesSystem\words.json", "SomeTile");
            System.Console.WriteLine("Words Successfully imported");

            
        }
    }
}