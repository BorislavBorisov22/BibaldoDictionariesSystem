using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionariesSystem.Framework.Core.Commands.Read
{
    public class ListWordInformationCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 2;
        private readonly IRepository<Dictionary> dictionaryRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider user;

        public ListWordInformationCommand(IRepository<Dictionary> dictionaryRepository,
                                            IUnitOfWork unitOfWork, IUserProvider user)
        {
            Guard.WhenArgument(dictionaryRepository, "dictionariyRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.dictionaryRepository = dictionaryRepository;
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

            string dictionaryName = parameters[0];

            string wordName = parameters[1];

            var dictionary = this.dictionaryRepository.All(d => d.Title == dictionaryName).FirstOrDefault();

            Guard.WhenArgument(dictionary, "No dictionary with that name was found.").IsNull().Throw();

            var word = dictionary.Words.FirstOrDefault(w => w.Name == wordName);

            Guard.WhenArgument(word, "No word with that name was found.").IsNull().Throw();

            StringBuilder wordInformation = new StringBuilder();

            wordInformation.AppendLine($"{word.Name} - {word.SpeechPart}");

            if (word.RootWord != null)
            {
                wordInformation.AppendLine($"{word.Name} comes from {word.RootWord}");
            }

            wordInformation.AppendLine($"{word.Name} means {string.Join(" also ", word.Meanings)}");

            return wordInformation.ToString();
        }
    }
}
