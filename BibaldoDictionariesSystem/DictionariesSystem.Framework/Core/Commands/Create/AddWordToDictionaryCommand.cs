using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
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
        public const int NumberOfParameters = 2;
        private readonly IRepository<Dictionary> dictionaries;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProvider user;

        public AddWordToDictionaryCommand(IRepository<Dictionary> dictionaries, IUnitOfWork unitOfWork, IUserProvider user)
        {
            Guard.WhenArgument(dictionaries, "dictionaries").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.dictionaries = dictionaries;
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
            string wordName = parameters[0];

            string dictionaryTitle = parameters[1];

            StringBuilder description = new StringBuilder();

            for (int i = 2; i < parameters.Count; i += 1)
            {
                if (i != ParametersCount - 1)
                {
                    description.Append(parameters[i] + ' ');
                }
                else
                {
                    description.Append(parameters[i]);
                }
            }

            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();

            Guard.WhenArgument(dictionary, "dictionary").IsNull().Throw();

            var foundWord = dictionary.Words.FirstOrDefault(w => w.Name == wordName);

            Guard.WhenArgument(foundWord, "foundWord").IsNotNull().Throw();

            var foundMeaning = dictionary.Meanings.FirstOrDefault(m => m.Description == description.ToString());

            Word newWord = new Word()
            {
                Name = wordName,
                Dictionary = dictionary,
                DictionaryId = dictionary.Id
            };

            if (foundMeaning == null)
            {
                var meaning = new Meaning()
                {
                    Description = description.ToString()
                };
                newWord.Meanings.Add(meaning);
            }
            else
            {
                newWord.Meanings.Add(foundMeaning);
            }

            dictionary.Words.Add(newWord);

            this.unitOfWork.SaveChanges();

            string result = $"A new word: {wordName} was added into dictionary: {dictionaryTitle}\n{wordName} means {description}";

            return result;
        }
    }
}
