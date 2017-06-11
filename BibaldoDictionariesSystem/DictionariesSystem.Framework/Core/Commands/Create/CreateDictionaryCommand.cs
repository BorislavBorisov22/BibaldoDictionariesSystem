using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Core.Commands.Create
{
    public class CreateDictionaryCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 2;

        private readonly IRepository<Dictionary> dictionariesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider userProvider;
        private readonly IDictionariesFactory dictionariesFactory;

        public CreateDictionaryCommand(IRepository<Dictionary> dictionariesRepository, IUnitOfWork unitOfWork, IUserProvider userProvider, IDictionariesFactory dictionariesFactory)
        {
            Guard.WhenArgument(dictionariesRepository, "repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(userProvider, "user").IsNull().Throw();

            this.dictionariesRepository = dictionariesRepository;
            this.unitOfWork = unitOfWork;
            this.userProvider = userProvider;
            this.dictionariesFactory = dictionariesFactory;
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

            string title = parameters[0];
            string languageName = parameters[1];

            string author = this.userProvider.LoggedUser.Username;

            var language = this.dictionariesFactory.GetLanguage(languageName);
            var newDictionary = this.dictionariesFactory.GetDictionary(title, author, language, DateProvider.Provider.GetDate());

            this.dictionariesRepository.Add(newDictionary);
            this.userProvider.LoggedUser.ContributionsCount += 3;

            this.unitOfWork.SaveChanges();

            string result = $"A new dictionary with title {title}, author {author} and language {languageName} was created.";
            return result;
        }
    }
}
