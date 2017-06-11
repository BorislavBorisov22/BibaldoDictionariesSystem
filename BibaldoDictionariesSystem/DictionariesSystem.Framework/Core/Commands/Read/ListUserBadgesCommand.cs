using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Models.Users;
using System.Collections.Generic;
using System.Text;

namespace DictionariesSystem.Framework.Core.Commands.Read
{
    public class ListUserBadgesCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "";
        private const int NumberOfParameters = 0;

        private readonly IUserProvider userProvider;

        public ListUserBadgesCommand(IUserProvider userProvider)
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
            Guard.WhenArgument(this.userProvider.IsLogged, "User is not logged in.").IsEqual(false).Throw();

            StringBuilder badges = new StringBuilder();
            int iteratorOfBadges = 0;
            int countOfBadges = this.userProvider.LoggedUser.Badges.Count;

            if (countOfBadges != 0)
            {
                badges.AppendLine($"{this.userProvider.LoggedUser.Username} has {countOfBadges} badges:");
                foreach (Badge badge in userProvider.LoggedUser.Badges)
                {
                    iteratorOfBadges++;
                    badges.AppendLine($"{iteratorOfBadges}: {badge.Name}");
                }
            }
            else
            {
                badges.AppendLine($"{this.userProvider.LoggedUser.Username} has no badges.");
            }

            return badges.ToString();
        }
    }
}
