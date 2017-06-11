using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;
using System.Linq;

namespace DictionariesSystem.Framework.Core.Commands.Update
{
    public class UpdateWordCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "[dictionaryTitle] [wordName] [newDescription]";
        private const int NumberOfParameters = 3;

        private readonly IRepository<Dictionary> dictionaries;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDictionariesFactory dictionaryFactory;

        public UpdateWordCommand(IRepository<Dictionary> dictionaries, IUnitOfWork unitOfWork, IDictionariesFactory dictionaryFactory)
        {
            Guard.WhenArgument(dictionaries, "dictionaries").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(dictionaryFactory, "dictionaryFactory").IsNull().Throw();

            this.dictionaries = dictionaries;
            this.unitOfWork = unitOfWork;
            this.dictionaryFactory = dictionaryFactory;
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
            string dictionaryTitle = parameters[0];
            string wordName = parameters[1];
            string newDescription = string.Join(" ", parameters.Skip(2));
            
            // TODO base.Execute();

            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();
            Guard.WhenArgument(dictionary, "No dictionary with this name was found.").IsNull().Throw();

            var word = dictionary.Words.FirstOrDefault(w => w.Name == wordName);
            Guard.WhenArgument(wordName, "No word with this name was found.").IsNull().Throw();

            word.Meanings.Add(this.dictionaryFactory.GetMeaning(newDescription));
            this.unitOfWork.SaveChanges();

            string result = $"Added new meaning to word {wordName}: {newDescription}";
            return result;
        }
    }
}
