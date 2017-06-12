using System;
using DictionariesSystem.Contracts.Core.Commands;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Data;
using Bytes2you.Validation;
using DictionariesSystem.Models.Dictionaries;
using System.Linq;
using System.Text;

namespace DictionariesSystem.Framework.Core.Commands.Common
{
    public class ContributorsCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "";
        private const int NumberOfParameters = 0;

        private readonly IRepository<Contributor> repository;

        public ContributorsCommand(IRepository<Contributor> repository)
        {
            Guard.WhenArgument(repository, "repository").IsNull().Throw();

            this.repository = repository;
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

            var builder = new StringBuilder();
            builder.AppendLine();

            this.repository.All(x => true)
                .ToList()
                .ForEach(x =>
                {
                    builder.AppendLine(x.FirstName + " " +  x.LastName + " " +  x.GithubProfile);
                });
            
            return builder.ToString();
        }
    }
}
