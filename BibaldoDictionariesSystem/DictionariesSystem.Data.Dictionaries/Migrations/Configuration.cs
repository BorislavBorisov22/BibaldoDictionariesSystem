namespace DictionariesSystem.Data.Dictionaries.Migrations
{
    using DictionariesSystem.Models.Dictionaries;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DictionariesSystem.Data.Dictionaries.DictionariesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DictionariesSystem.Data.Dictionaries.DictionariesDbContext context)
        {
            context.Contributors.AddOrUpdate(x => x.FirstName,
                new Contributor()
                {
                    FirstName = "Rosen",
                    LastName = "Urkov",
                    GithubProfile = "https://github.com/RosenUrkov"
                },
                new Contributor()
                {
                    FirstName = "Borislav",
                    LastName = "Borisov",
                    GithubProfile = "https://github.com/BorislavBorisov22"
                },
                new Contributor()
                {
                    FirstName = "Martin",
                    LastName = "Kamenov",
                    GithubProfile = "https://github.com/MartinKamenov"
                });
        }
    }
}
