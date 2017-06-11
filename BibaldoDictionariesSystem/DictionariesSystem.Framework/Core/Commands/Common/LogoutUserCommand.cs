using System.Collections.Generic;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using Bytes2you.Validation;

namespace DictionariesSystem.Framework.Core.Commands.Common
{
    public class LogoutUserCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "";
        private const int NumberOfParameters = 0;

        private readonly IUserProvider userProvider;

        public LogoutUserCommand(IUserProvider userProvider)
        {
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

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
            base.Execute(parameters);

            this.userProvider.Logout();

            return $"You have logged out successfully!";
        }
    }
}
