using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;
using System;

namespace DictionariesSystem.Framework.Core.Commands.Create
{
    public class CreateDictionaryCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 2;

        private readonly IRepository<Dictionary> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider user;

        public CreateDictionaryCommand(IRepository<Dictionary> repository, IUnitOfWork unitOfWork, IUserProvider user)
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

            Guard.WhenArgument(parameters.Count, "parameters").IsNotEqual(2).Throw();

            string title = parameters[0];

            string languageName = parameters[1];

            string author = user.LoggedUser.Username;
        
            Language language = new Language()
            {
                Name = languageName
            };

            Dictionary dictionary = new Dictionary()
            {
                Author = author,
                Title = title,
                Language = language
            };

            this.repository.Add(dictionary);

            this.user.LoggedUser.ContributionsCount += 1;

            string result = $"A new dictionary with title {title}, author {author} and language {languageName} was created.";

            return result;
        }
    }
}
