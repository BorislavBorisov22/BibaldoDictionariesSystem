using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Core.Commands.Read
{
    public class LoginCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "[username] [password]";
        private const int NumberOfParameters = 2;

        private readonly IUserProvider userProvider;

        public LoginCommand(IUserProvider userProvider)
        {
            Guard.WhenArgument(userProvider, "user").IsNull().Throw();

            this.userProvider = userProvider;
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
            string username = parameters[0];
            string passHash = parameters[1];

            base.Execute(parameters);
     
            this.userProvider.Login(username, passHash);

            return $"User {username} has logged successfully.";
        }
    }
}
