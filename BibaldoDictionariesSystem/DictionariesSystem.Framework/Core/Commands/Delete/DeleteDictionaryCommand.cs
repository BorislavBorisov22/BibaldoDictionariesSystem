using System;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using Bytes2you.Validation;
using System.Linq;

namespace DictionariesSystem.Framework.Core.Commands.Delete
{
    public class DeleteDictionaryCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 1;
        private readonly IRepository<Dictionary> dictionaries;
        private readonly IUnitOfWork unitOfWork;

        public DeleteDictionaryCommand(IRepository<Dictionary> dictionaries, IUnitOfWork unitOfWork)
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

            var dictionary = this.dictionaries.All(d => d.Title == dictionaryTitle).FirstOrDefault();
            Guard.WhenArgument(dictionary, "No dictionary with this name.").IsNull().Throw();

            dictionaries.Delete(dictionary);
            this.unitOfWork.SaveChanges();

            string result = $"Deleted dictionary {dictionary.Title}";
            return result;
        }
    }
}
