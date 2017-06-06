using DictionariesSystem.Data.Dictionaries;
using DictionariesSystem.Data.Logs;
using DictionariesSystem.Data.Users;
using System.Linq;

namespace DictionariesSystem.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            var loggingDbContext = new LogsDbContext();
            loggingDbContext.ExceptionLogs.FirstOrDefault();

            var dictionaryDbContext = new DictionariesDbContext();
            dictionaryDbContext.Database.CreateIfNotExists();

            var usersDbContext = new UsersDbContext();
            usersDbContext.Database.CreateIfNotExists();
        }
    }
}
