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
    public class ListUserBadgesCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 0;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        public ListUserBadgesCommand(IUnitOfWork unitOfWork, IUserProvider userProvider)
        {
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.unitOfWork = unitOfWork;
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
