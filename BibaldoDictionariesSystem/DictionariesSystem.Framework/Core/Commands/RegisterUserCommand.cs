using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.Framework.Core.Commands
{
    public class RegisterUserCommand : BaseCommand, ICommand
    {
        public const int NumberOfParameters = 2;
        private readonly IRepository<User> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider user;

        public RegisterUserCommand(IRepository<User> repository, IUnitOfWork unitOfWork, IUserProvider user)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.user = user;
        }

        protected override int ParametersCount
        {
            get
            {
                return NumberOfParameters;
            }
        }

        public override string Execute(IList<string> parameters)
        {
            base.Execute(parameters);

            string username = parameters[0];
            string password = parameters[1];

            this.user.Login(username, password);

            string restult = $"A new user with username: {username} was created and logged in.";

            return restult;
        }
    }
}
