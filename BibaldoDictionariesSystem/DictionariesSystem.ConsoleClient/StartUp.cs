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

            for (int i = 0; i < 10; i++)
            {
                loggingDbContext.ExceptionLogs.Add(new ExceptionLog
                {
                    Message = "Another test" + i,
                    LoggedOn = DateTime.Now
                });
            }

            loggingDbContext.SaveChanges();
        }
    }
}
