using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DictionariesSystem.Framework.Core.Commands.Delete
{
    public class DeleteWordCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 2;
        private readonly IRepository<Dictionary> dictionaries;
        private readonly IUnitOfWork unitOfWork;

        public DeleteWordCommand(IRepository<Dictionary> dictionaries, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(dictionaries, "dictionaries").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.dictionaries = dictionaries;
            this.unitOfWork = unitOfWork;
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

            string dictionaryTitle = parameters[0];

            string wordName = parameters[1];

            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();

            Guard.WhenArgument(dictionary, "No dictionary with this name.").IsNull().Throw();

            var word = dictionary.Words.FirstOrDefault(w => w.Name == wordName);

            Guard.WhenArgument(word, "No word with this name.").IsNull().Throw();

            dictionary.Words.Remove(word);

            this.unitOfWork.SaveChanges();

            var result = $"Deleted word: {word.Name} from dictionary: {dictionary.Title}";

            return result;
        }
    }
}
