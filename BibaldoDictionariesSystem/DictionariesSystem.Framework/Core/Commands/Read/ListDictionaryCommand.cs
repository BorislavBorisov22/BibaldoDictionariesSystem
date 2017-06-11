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

namespace DictionariesSystem.Framework.Core.Commands.Read
{
    public class ListDictionaryCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 1;
        private readonly IRepository<Dictionary> dictionaries;

        public ListDictionaryCommand(IRepository<Dictionary> dictionaries)
        {
            Guard.WhenArgument(dictionaries, "dictionaries").IsNull().Throw();

            this.dictionaries = dictionaries;
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

            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();
            Guard.WhenArgument(dictionary, "No dictionary with this name").IsNull().Throw();

            StringBuilder words = new StringBuilder();
            foreach (Word word in dictionary.Words)
            {
                words.AppendLine($"{word.Name} : {string.Join(", ", word.Meanings)}");
            }

            return words.ToString();
        }
    }
}
