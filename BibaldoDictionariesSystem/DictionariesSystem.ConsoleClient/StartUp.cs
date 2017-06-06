using DictionariesSystem.Data.Logs;
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
                Message = "Test Logging Message",
                LoggedOn = DateTime.Now
            };

            loggingDbContext.ExceptionLogs.Add(exceptionLog);
            loggingDbContext.SaveChanges();

        }
    }
}
