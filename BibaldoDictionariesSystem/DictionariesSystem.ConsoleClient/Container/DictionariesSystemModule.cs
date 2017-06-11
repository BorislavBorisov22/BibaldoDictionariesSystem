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
        public const string GeneratePdfReportCommandName = "GenerateReport";
        public const string ListWordInformationCommandName = "ShowWordInfo";
        public const string ListDictionaryCommandName = "ShowDictionaryInfo";
        public const string ListUserBadgesCommandName = "ShowUserBadges";
        public const string LoginUserCommandName = "Login";

        // update
        public const string UpdateWordCommandName = "UpdateWord";
        public const string ImportWordsFromFileCommandName = "ImportWords";

        public override void Load()
        {
            // contexts
            this.Bind<LogsDbContext>().ToSelf().InSingletonScope();
            this.Bind<UsersDbContext>().ToSelf().InSingletonScope();
            this.Bind<DictionariesDbContext>().ToSelf().InSingletonScope();

            // units of work depending on context
            this.Bind<IUnitOfWork>().To<UnitOfWork>()
                .InSingletonScope().Named(LogsUnitOfWorkName)
                .WithConstructorArgument("context", this.Kernel.Get<LogsDbContext>());

            this.Bind<IUnitOfWork>().To<UnitOfWork>()
                .InSingletonScope().Named(UsersUnitOfWorkName)
                .WithConstructorArgument("context", this.Kernel.Get<UsersDbContext>());

            this.Bind<IUnitOfWork>().To<UnitOfWork>()
                .InSingletonScope().Named(DictionariesUnitOfWorkName)
                .WithConstructorArgument("context", this.Kernel.Get<DictionariesDbContext>());

            // repositories
            // from users db context
            this.Bind<IRepository<User>>().To<Repository<User>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<UsersDbContext>());

            this.Bind<IRepository<Badge>>().To<Repository<Badge>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<UsersDbContext>());

            // from logs db context
            this.Bind<IRepository<ExceptionLog>>().To<Repository<ExceptionLog>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<LogsDbContext>());

            this.Bind<IRepository<UserLog>>().To<Repository<UserLog>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<LogsDbContext>());

            // from dictionaries db context
            this.Bind<IRepository<Word>>().To<Repository<Word>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DictionariesDbContext>());

            this.Bind<IRepository<Dictionary>>().To<Repository<Dictionary>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DictionariesDbContext>());

            this.Bind<IRepository<Language>>().To<Repository<Language>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DictionariesDbContext>());

            this.Bind<IRepository<Meaning>>().To<Repository<Meaning>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DictionariesDbContext>());

            // engine and engine depndencies
            this.Bind<IEngine>().To<Engine>().InSingletonScope();
            this.Bind<ICommandProcessor>().To<CommandProcessor>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            this.Bind<ILogger>().To<ExceptionLogger>().WhenInjectedInto<IEngine>()
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(LogsUnitOfWorkName));

            this.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
            this.Bind<IUserProvider>().To<UserProvider>().InSingletonScope()
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(UsersUnitOfWorkName));

            // importers and reporters
            this.Bind<IWordsImporterProvider>().To<XmlWordsImporterProvider>().Named(XmlWordsImporterName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

            this.Bind<IWordsImporterProvider>().To<JsonWordsImporterProvider>().Named(JsonWordsImporterName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

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
            .InSingletonScope()
            .NamedLikeFactoryMethod((ICommandFactory factory) => factory.GetCommand(null));

            // commands
            // create
            this.Bind<ICommand>().To<CreateDictionaryCommand>().Named(CreateDictionaryCommandName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

            this.Bind<ICommand>().To<AddWordToDictionaryCommand>().Named(AddWordToDictionaryCommandName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

            this.Bind<ICommand>().To<RegisterUserCommand>().Named(RegisterUserCommandName);

            // delete
            this.Bind<ICommand>().To<DeleteDictionaryCommand>().Named(DeleteDictionaryCommandName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

            this.Bind<ICommand>().To<DeleteWordCommand>().Named(DeleteWordCommandName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

            // read
            this.Bind<ICommand>().To<GeneratePdfReportCommand>().Named(GeneratePdfReportCommandName);
            this.Bind<ICommand>().To<ListWordInformationCommand>().Named(ListWordInformationCommandName);
            this.Bind<ICommand>().To<ListDictionaryCommand>().Named(ListDictionaryCommandName);
            this.Bind<ICommand>().To<ListUserBadgesCommand>().Named(ListUserBadgesCommandName);
            this.Bind<ICommand>().To<LoginUserCommand>().Named(LoginUserCommandName);

            // update
            this.Bind<ICommand>().To<UpdateWordCommand>().Named(UpdateWordCommandName)
                .WithConstructorArgument("unitOfWork", this.Kernel.Get<IUnitOfWork>(DictionariesUnitOfWorkName));

            this.Bind<ICommand>().To<ImportWordsFromFileCommand>().Named(ImportWordsFromFileCommandName);
        }
    }
}