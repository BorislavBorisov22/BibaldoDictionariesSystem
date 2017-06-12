using System;
using Ninject.Extensions.Interception;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Models.Users;
using System.Linq;

namespace DictionariesSystem.ConsoleClient.Interceptors
{
    public class UserContributionsInterceptor : IInterceptor
    {
        private readonly IUserProvider userProvider;
        private readonly IRepository<User> users;
        private readonly IRepository<Badge> badges;
        private readonly IUnitOfWork unitOfWork;

        public UserContributionsInterceptor(IUserProvider userProvider, IRepository<User> users, IRepository<Badge> badges, IUnitOfWork unitOfWork)
        {
            this.userProvider = userProvider;
            this.users = users;
            this.badges = badges;
            this.unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            var userId = this.userProvider.LoggedUser.Id;
            var user = this.users.All(x => x.Id == userId).First();

            user.ContributionsCount++;

            this.badges
                .All(x => true)
                .ToList()
                .ForEach(x =>
                {
                    if (!user.Badges.Any(y => y.Name == x.Name) &&
                        user.ContributionsCount >= x.RequiredContributions)
                    {
                        user.Badges.Add(x);
                    }
                });

            this.unitOfWork.SaveChanges();
            invocation.Proceed();
        }
    }
}
