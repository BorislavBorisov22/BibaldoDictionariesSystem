using System;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using Bytes2you.Validation;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Loaders;

namespace DictionariesSystem.Framework.Core.Commands.Read
{
    public class GenerateUsersReportCommand : BaseCommand, ICommand
    {
        public const string ParametersNames = "";
        private const int NumberOfParameters = 0;

        private readonly IPdfReporterProvider reporterProvider;

        public GenerateUsersReportCommand(IPdfReporterProvider reporterProvider)
        {
            Guard.WhenArgument(reporterProvider, "reporterProvider").IsNull().Throw();

            this.reporterProvider = reporterProvider;
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
            this.reporterProvider.CreateReport();

            return $"Report created sucessfully! Opening...";
        }
    }
}
