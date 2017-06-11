using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Contracts.Core.Commands;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Data.Common;
using DictionariesSystem.Data.Dictionaries;
using DictionariesSystem.Data.Logs;
using DictionariesSystem.Data.Users;
using DictionariesSystem.Framework.Core;
using DictionariesSystem.Framework.Core.Commands.Create;
using DictionariesSystem.Framework.Core.Commands.Delete;
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Framework.Core.Commands.Update;
using DictionariesSystem.Framework.Core.Factories;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Framework.Loaders;
using DictionariesSystem.Models.Dictionaries;
using DictionariesSystem.Models.Logs;
using DictionariesSystem.Models.Users;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DictionariesSystem.ConsoleClient.Container
{
    public class DictionariesSystemModule : NinjectModule
    {
        private const string UsersDbContextName = "UsersDbContext";
        private const string LogsDbContextName = "LogsDbContext";
        private const string DictionariesDbContextName = "DictionariesDbContext";

        private const string JsonWordsImporterName = "json";
        private const string XmlWordsImporterName = "xml";

        private const string LogsUnitOfWorkName = "LogsUnitOfWork";
        private const string UsersUnitOfWorkName = "UsersUnitOfWork";
        private const string DictionariesUnitOfWorkName = "DictionariesUnitOfWork";

        // create
        public const string CreateDictionaryCommandName = "CreateDictionary";
        public const string AddWordToDictionaryCommandName = "AddWordToDictionary";
        public const string RegisterUserCommandName = "Register";

        // delete
        public const string DeleteDictionaryCommandName = "DeleteDictionary";
        public const string DeleteWordCommandName = "DeleteWord";

        // read
        public const string ListDictionaryCommandName = "ShowDictionaryInfo";
        public const string ListWordInformationCommandName = "ShowWordInfo";
        public const string ListUserBadgesCommandName = "ShowBadges";
        public const string GeneratePdfReportCommandName = "GenerateUsersReport";
        public const string LoginUserCommandName = "Login";

        // update
        public const string UpdateWordCommandName = "UpdateWord";
        public const string ImportWordsFromFileCommandName = "ImportWords";

        public override void Load()
        {
            // contexts
            this.Bind<DbContext>().To<LogsDbContext>().InSingletonScope().Named(LogsDbContextName);
            this.Bind<DbContext>().To<UsersDbContext>().InSingletonScope().Named(UsersDbContextName);
            this.Bind<DbContext>().To<DictionariesDbContext>().InSingletonScope().Named(DictionariesDbContextName);

            // units of work depending on context
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            // repositories
            // from users db context
            this.Bind<IRepository<User>>().To<Repository<User>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(UsersDbContextName));

            this.Bind<IRepository<Badge>>().To<Repository<Badge>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(UsersDbContextName));

            // from logs db context
            this.Bind<IRepository<ExceptionLog>>().To<Repository<ExceptionLog>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(LogsDbContextName));

            this.Bind<IRepository<UserLog>>().To<Repository<UserLog>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(UsersDbContextName));

            // from dictionaries db context
            this.Bind<IRepository<Word>>().To<Repository<Word>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(DictionariesDbContextName));

            this.Bind<IRepository<Dictionary>>().To<Repository<Dictionary>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(DictionariesDbContextName));

            this.Bind<IRepository<Language>>().To<Repository<Language>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(DictionariesDbContextName));

            this.Bind<IRepository<Meaning>>().To<Repository<Meaning>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(DictionariesDbContextName));

            // engine and engine depndencies
            this.Bind<IEngine>().To<Engine>().InSingletonScope();
            this.Bind<ICommandProcessor>().To<CommandProcessor>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            this.Bind<ILogger>().To<ExceptionLogger>().WhenInjectedInto<IEngine>();

            this.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
            this.Bind<IUserProvider>().To<UserProvider>().InSingletonScope();

            // importers and reporters
            this.Bind<IWordsImporterProvider>().To<XmlWordsImporterProvider>().Named(XmlWordsImporterName);

            this.Bind<IWordsImporterProvider>().To<JsonWordsImporterProvider>().Named(JsonWordsImporterName);

            this.Bind<IPdfReporterProvider>().To<PdfReporterProvider>();

            // models factories
            this.Bind<IDictionariesFactory>().To<DictionariesFactory>().InSingletonScope();
            this.Bind<ILogsFactory>().To<LogsFactory>().InSingletonScope();
            this.Bind<IUserFactory>().To<UserFactory>().InSingletonScope();

            // importers factory
            this.Bind<IWordsImporterProvider>().ToMethod(context =>
            {
                IList<IParameter> parameters = context.Parameters.ToList();
                string importerName = (string)parameters[0].GetValue(context, null);

                var importer = context.Kernel.Get<IWordsImporterProvider>(importerName);

                return importer;
            }).NamedLikeFactoryMethod((IWordsImporterFactory factory) => factory.GetImporter(null));

            // commands factory
            this.Bind<ICommandFactory>().ToFactory();
            this.Bind<ICommand>().ToMethod(context =>
            {
                IList<IParameter> parameters = context.Parameters.ToList();
                string importerName = (string)parameters[0].GetValue(context, null);

                var importer = context.Kernel.Get<ICommand>(importerName);

                return importer;
            })
            .NamedLikeFactoryMethod((ICommandFactory factory) => factory.GetCommand(null));

            // commands
            // create
            this.Bind<ICommand>().To<CreateDictionaryCommand>().Named(CreateDictionaryCommandName);
            this.Bind<ICommand>().To<AddWordToDictionaryCommand>().Named(AddWordToDictionaryCommandName);
            this.Bind<ICommand>().To<RegisterUserCommand>().Named(RegisterUserCommandName);

            // delete
            this.Bind<ICommand>().To<DeleteDictionaryCommand>().Named(DeleteDictionaryCommandName);
            this.Bind<ICommand>().To<DeleteWordCommand>().Named(DeleteWordCommandName);

            // read
            this.Bind<ICommand>().To<GeneratePdfReportCommand>().Named(GeneratePdfReportCommandName);
            this.Bind<ICommand>().To<ListWordInformationCommand>().Named(ListWordInformationCommandName);
            this.Bind<ICommand>().To<ListDictionaryCommand>().Named(ListDictionaryCommandName);
            this.Bind<ICommand>().To<ListUserBadgesCommand>().Named(ListUserBadgesCommandName);
            this.Bind<ICommand>().To<LoginUserCommand>().Named(LoginUserCommandName);

            // update
            this.Bind<ICommand>().To<UpdateWordCommand>().Named(UpdateWordCommandName);
            this.Bind<ICommand>().To<ImportWordsFromFileCommand>().Named(ImportWordsFromFileCommandName);
        }
    }
}