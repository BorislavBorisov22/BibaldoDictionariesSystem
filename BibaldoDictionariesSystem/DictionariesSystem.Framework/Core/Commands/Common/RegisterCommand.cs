using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Users;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Core.Commands.Create
{
    public class RegisterCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "[username] [password]";
        private const int NumberOfParameters = 2;

        private readonly IRepository<User> repository;
        private readonly IUserProvider userProvider;

        public RegisterCommand(IRepository<User> repository, IUserProvider user)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.repository = repository;
            this.userProvider = user;
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

            this.userProvider.Register(username, password);

            return $"A new user with username {username} was created!";            
        }
    }
}
