using System;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Logs;
using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Factories;

namespace DictionariesSystem.Framework.Core.Providers.Loggers
{
    public class UserLogger : ILogger
    {
        private readonly IRepository<UserLog> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly ILogsFactory logsFactory;

        public UserLogger(IRepository<UserLog> repository, IUnitOfWork unitOfWork, IUserProvider userProvider, ILogsFactory logsFactory)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();
            Guard.WhenArgument(logsFactory, "logsFactory").IsNull().Throw();

            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.logsFactory = logsFactory;
        }

        public void Log(string information)
        {
            var userLog = this.logsFactory.GetUserLog(
                this.userProvider.LoggedUser.Username,
                information,
                DateProvider.Provider.GetDate());

            this.repository.Add(userLog);
            this.unitOfWork.SaveChanges();
        }
    }
}
