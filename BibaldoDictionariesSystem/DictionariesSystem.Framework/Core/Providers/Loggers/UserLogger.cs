using System;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Logs;
using Bytes2you.Validation;
using DictionariesSystem.Models.Logs.Enums;

namespace DictionariesSystem.Framework.Core.Providers.Loggers
{
    public class UserLogger : ILogger
    {
        private readonly IRepository<UserLog> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;

        public UserLogger(IRepository<UserLog> repository, IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
        }
        
        public void Log(string message, string state = null)
        {
            var action = (UserAction)Enum.Parse(typeof(UserAction),state);

            this.repository.Add(new UserLog()
            {
                Username = this.userProvider.LoggedUser.Username,
                Action = action,
                LoggedOn = DateProvider.Provider.GetDate(),
                Message = message
            });

            this.unitOfWork.SaveChanges();
        }
    }
}
