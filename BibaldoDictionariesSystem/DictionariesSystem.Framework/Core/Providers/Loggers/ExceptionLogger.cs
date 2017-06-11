using System;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using Bytes2you.Validation;
using DictionariesSystem.Models.Logs;
using DictionariesSystem.Contracts.Core.Factories;

namespace DictionariesSystem.Framework.Core.Providers.Loggers
{
    public class ExceptionLogger : ILogger
    {
        private readonly IRepository<ExceptionLog> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogsFactory logsFactory;
        
        public ExceptionLogger(IRepository<ExceptionLog> repository, IUnitOfWork unitOfWork, ILogsFactory logsFactory)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(logsFactory, "logsFactory").IsNull().Throw();
            
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logsFactory = logsFactory;
        }

        public void Log(string information)
        {
            var exceptionLog = this.logsFactory.GetExceptionLog(information, DateProvider.Provider.GetDate());
            this.repository.Add(exceptionLog);
            this.unitOfWork.SaveChanges();
        }
    }
}
