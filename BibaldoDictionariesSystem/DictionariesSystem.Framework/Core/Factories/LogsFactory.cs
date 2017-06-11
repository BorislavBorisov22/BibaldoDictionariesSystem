using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Models.Logs;
using System;

namespace DictionariesSystem.Framework.Core.Factories
{
    public class LogsFactory : ILogsFactory
    {
        public ExceptionLog GetExceptionLog(string message, DateTime loggedOn)
        {
            return new ExceptionLog() { Message = message, LoggedOn = loggedOn };
        }

        public UserLog GetUserLog(string username, string commandName, DateTime executionDate)
        {
            return new UserLog() { Username = username, CommandName = commandName,  ExecutionDate = executionDate };
        }
    }
}
