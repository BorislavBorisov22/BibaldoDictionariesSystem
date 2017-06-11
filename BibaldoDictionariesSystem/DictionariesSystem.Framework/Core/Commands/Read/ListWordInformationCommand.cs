﻿using Bytes2you.Validation;
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
        public const string ParametersNames = "[dictionaryName] [wordName]";
        private const int NumberOfParameters = 2;

        private readonly IRepository<Dictionary> dictionaryRepository;
        private readonly IUserProvider userProvider;

        public ListWordInformationCommand(IRepository<Dictionary> dictionaryRepository, IUserProvider userProvider)
        {
            Guard.WhenArgument(dictionaryRepository, "dictionariyRepository").IsNull().Throw();
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.dictionaryRepository = dictionaryRepository;
            this.userProvider = userProvider;
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
