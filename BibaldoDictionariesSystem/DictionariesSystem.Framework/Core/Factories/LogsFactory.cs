using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Models.Logs;
using DictionariesSystem.Models.Logs.Enums;
using System;

namespace DictionariesSystem.Framework.Core.Factories
{
    public class LogsFactory : ILogsFactory
    {
        public ExceptionLog GetExceptionLog(string message, DateTime loggedOn)
        {
            return new ExceptionLog() { Message = message, LoggedOn = loggedOn };
        }

        public UserLog GetUserLog(string username, string message, string action, DateTime loggedOn)
        {
            UserAction userAction;
            Enum.TryParse(action, out userAction);

            return new UserLog() { Username = username, Message = message, Action = userAction, LoggedOn = loggedOn };
        }
    }
}
