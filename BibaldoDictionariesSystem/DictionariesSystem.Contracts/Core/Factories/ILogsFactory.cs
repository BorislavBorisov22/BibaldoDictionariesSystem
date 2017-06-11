using DictionariesSystem.Models.Logs;
using System;

namespace DictionariesSystem.Contracts.Core.Factories
{
    public interface ILogsFactory
    {
        UserLog GetUserLog(string username, string commandName, DateTime executionDate);

        ExceptionLog GetExceptionLog(string message, DateTime loggedOn);
    }
}