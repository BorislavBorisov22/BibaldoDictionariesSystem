using DictionariesSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DictionariesSystem.Data.Users.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<UsersDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UsersDbContext context)
        {
            context.Users.AddOrUpdate(x => x.Username,
                new User()
                {
                    Username = "Bibaldo",
                    Passhash = "bibi",
                    ContributionsCount = 300,
                    Badges = new HashSet<Badge>() {
                        new Badge() { Name = "Caveman", RequiredContributions = 0 },
                        new Badge() { Name = "Newbie", RequiredContributions = 20 },
                        new Badge() { Name = "Book wyrm", RequiredContributions = 50 },
                        new Badge() { Name = "Crosswords master", RequiredContributions = 100 },
                        new Badge() { Name = "Linguistics bachelor", RequiredContributions = 200 }
                        }
                });
        }
    }
}
