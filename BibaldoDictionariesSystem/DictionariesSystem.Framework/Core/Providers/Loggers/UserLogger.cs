using System;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Logs;
using Bytes2you.Validation;

namespace DictionariesSystem.Framework.Core.Providers.Loggers
{
    public class UserLogger : ILogger
    {
        private readonly IRepository<UserLog> repository;
        private readonly IUnitOfWork unitOfWork;

        public UserLogger(IRepository<UserLog> repository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Log(string message)
        {
            
        }
    }
}
