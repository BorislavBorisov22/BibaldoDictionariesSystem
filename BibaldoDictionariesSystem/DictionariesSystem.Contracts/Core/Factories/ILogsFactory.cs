using DictionariesSystem.Models.Logs;
using System;

namespace DictionariesSystem.Contracts.Core.Factories
{
    public interface ILogsFactory
    {
        UserLog GetUserLog(string username, string message, string action, DateTime loggedOn);

        ExceptionLog GetExceptionLog(string message, DateTime loggedOn);
    }
}