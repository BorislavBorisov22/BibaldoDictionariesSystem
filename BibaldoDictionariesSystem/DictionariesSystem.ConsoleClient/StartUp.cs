using DictionariesSystem.Data.Dictionaries;
using DictionariesSystem.Data.Logs;
using DictionariesSystem.Data.Users;
using DictionariesSystem.Models.Logs;
using System;
using System.Linq;

namespace DictionariesSystem.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            var loggingDbContext = new LogsDbContext();
            loggingDbContext.ExceptionLogs.FirstOrDefault();

            var exceptionLog = new ExceptionLog
            {
                Message = "My Test",
                LoggedOn = DateTime.Now
            };

            loggingDbContext.ExceptionLogs.Add(exceptionLog);
            loggingDbContext.SaveChanges();

            var dictionaryDbContext = new DictionariesDbContext();
            dictionaryDbContext.Database.CreateIfNotExists();
            dictionaryDbContext.SaveChanges();
        }
    }
}
