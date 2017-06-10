using System;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using Bytes2you.Validation;
using DictionariesSystem.Models.Logs;

namespace DictionariesSystem.Framework.Core.Providers.Loggers
{
    public class ExceptionLogger : ILogger
    {
        private readonly IRepository<ExceptionLog> repository;
        private readonly IUnitOfWork unitOfWork;

        public ExceptionLogger(IRepository<ExceptionLog> repository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Log(string message, string state = null)
        {
            this.repository.Add(new ExceptionLog() { Message = message, LoggedOn = DateProvider.Provider.GetDate() });
            this.unitOfWork.SaveChanges();
        }
    }
}
