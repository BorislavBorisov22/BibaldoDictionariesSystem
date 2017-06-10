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

namespace DictionariesSystem.Framework.Core.Commands.Read
{
    public class LoginUserCommand : BaseCommand, ICommand
    {
        public const int NumberOfParameters = 2;
        private readonly IUserProvider user;
        private readonly IRepository<User> users;

        public LoginUserCommand(IRepository<User> users, IUserProvider user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();
            Guard.WhenArgument(users, "users").IsNull().Throw();

            this.user = user;
            this.users = users;
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

            var foundUser = this.users.All(u => u.Username == username).FirstOrDefault();

            Guard.WhenArgument(foundUser, "foundUser").IsNull().Throw();

            Guard.WhenArgument(passHash, "passHash").IsNotEqual(foundUser.Passhash).Throw();

            this.user.Login(username, passHash);

            return $"User {username} has logged in.";
        }
    }
}
