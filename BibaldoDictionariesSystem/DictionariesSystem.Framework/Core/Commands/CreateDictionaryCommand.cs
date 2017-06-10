using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Models.Dictionaries;
using System.Collections.Generic;

namespace DictionariesSystem.Framework.Core.Commands
{
    public class CreateDictionaryCommand : ICommand
    {
        private readonly IRepository<Dictionary> repository;
        private readonly IUnitOfWork unitOfWork;

        public CreateDictionaryCommand(IRepository<Dictionary> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public string Execute(IList<string> parameters)
        {
            string title = parameters[0];

            string author = parameters[1];
        
            string languageName = parameters[2];

            Language language = new Language()
            {
                Name = languageName
            };

            Dictionary dictionary = new Dictionary()
            {
                Author = author,
                Title = title,
                Language = language
            };

            this.repository.Add(dictionary);

            string result = $"A ne dictionary with title {title}, author {author} and language {languageName} was created.";

            return result;
        }
    }
}
