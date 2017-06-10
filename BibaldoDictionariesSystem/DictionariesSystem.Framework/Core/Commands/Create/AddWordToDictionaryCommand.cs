using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionariesSystem.Framework.Core.Commands.Create
{
    public class AddWordToDictionaryCommand : BaseCommand, ICommand
    {
        private  const int NumberOfParameters = 2;

        private readonly IRepository<Dictionary> dictionaries;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider user;
        private readonly IDictionariesFactory dictionariesFactory;

        public AddWordToDictionaryCommand(IRepository<Dictionary> dictionaries, IUnitOfWork unitOfWork, IUserProvider user, IDictionariesFactory dictionariesFactory)
        {
            Guard.WhenArgument(dictionaries, "dictionaries").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(user, "user").IsNull().Throw();
            Guard.WhenArgument(dictionariesFactory, "dictionariesFactory").IsNull().Throw();

            this.dictionaries = dictionaries;
            this.unitOfWork = unitOfWork;
            this.user = user;
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
            string wordName = parameters[0];
            string dictionaryTitle = parameters[1];
            string speechPart = parameters[2];
            string wordDescription = string.Join(" ", parameters.Skip(3));
           
            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();
            Guard.WhenArgument(dictionary, "No Such Dictionary in the system").IsNull().Throw();

            var foundWord = dictionary.Words.FirstOrDefault(w => w.Name == wordName);
            Guard.WhenArgument(foundWord, "Word Already exists").IsNotNull().Throw();

            Meaning wordMeaning = dictionary.Meanings.FirstOrDefault(m => m.Description == wordDescription);

            if (wordDescription == null)
            {
                wordMeaning = this.dictionariesFactory.GetMeaning(wordDescription);
            }

            Word newWord = this.dictionariesFactory.GetWord(wordName, speechPart);
            newWord.Meanings.Add(wordMeaning);
            dictionary.Words.Add(newWord);

            this.unitOfWork.SaveChanges();

            string result = $"A new word: {wordName} was added into dictionary: {dictionaryTitle}\n{wordName} means {wordDescription}";
            return result;
        }
    }
}
