using Bytes2you.Validation;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Loaders;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Core.Commands.Update
{
    public class ImportWordsFromFileCommand : BaseCommand, ICommand
    {
        private const int NumberOfParameters = 3;

        private readonly IWordsImporterFactory wordsImporterFactory;

        public ImportWordsFromFileCommand(IWordsImporterFactory wordsImporterFactory)
        {
            Guard.WhenArgument(wordsImporterFactory, "wordsImporterFactory").IsNull().Throw();

            this.wordsImporterFactory = wordsImporterFactory;
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

            string fileType = parameters[0];
            string dictionaryName = parameters[1];
            string filePath = parameters[2];

            var importer = this.wordsImporterFactory.GetImporter(fileType);
            importer.Import(filePath, dictionaryName);

            return $"Words from {filePath} have been successfuly imported into the system";
        }
    }
}