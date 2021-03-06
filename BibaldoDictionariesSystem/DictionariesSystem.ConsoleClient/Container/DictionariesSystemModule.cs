﻿using DictionariesSystem.ConsoleClient.Configuration;
using DictionariesSystem.ConsoleClient.Interceptors;
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
using DictionariesSystem.Framework.Core.Commands.Common;
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
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Parameters;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DictionariesSystem.ConsoleClient.Container
{
    public class DictionariesSystemModule : NinjectModule
    {
        // contexts
        private const string UsersDbContextName = "UsersDbContext";
        private const string LogsDbContextName = "LogsDbContext";
        private const string DictionariesDbContextName = "DictionariesDbContext";

        // import file formats
        private const string JsonWordsImporterName = "json";
        private const string XmlWordsImporterName = "xml";

        // commands
        // create
        public const string CreateDictionaryCommandName = "CreateDictionary";
        public const string AddWordToDictionaryCommandName = "AddWordToDictionary";

        // delete
        public const string DeleteDictionaryCommandName = "DeleteDictionary";
        public const string DeleteWordCommandName = "DeleteWord";

        // read
        public const string ListDictionaryCommandName = "ListDictionary";
        public const string ListWordInformationCommandName = "ListWordInformation";
        public const string ListUserBadgesCommandName = "ListBadges";

        // update
        public const string UpdateWordCommandName = "UpdateWord";
        public const string ImportWordsFromFileCommandName = "ImportWordsFromFile";

        // common
        public const string GeneratePdfReportCommandName = "GenerateUsersReport";
        public const string RegisterUserCommandName = "Register";
        public const string LoginUserCommandName = "Login";
        public const string LogoutCommandName = "Logout";
        public const string ContributorsCommandName = "Contributors";
        public const string ClearCommandName = "Clear";
        public const string HelpCommandName = "--help";

        public override void Load()
        {
            // contexts
            this.Bind<DbContext>().To<LogsDbContext>().InSingletonScope().Named(LogsDbContextName);
            this.Bind<DbContext>().To<UsersDbContext>().InSingletonScope().Named(UsersDbContextName);
            this.Bind<DbContext>().To<DictionariesDbContext>().InSingletonScope().Named(DictionariesDbContextName);

            // unit of work
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
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(LogsDbContextName));

            // from dictionaries db context
            this.Bind<IRepository<Contributor>>().To<Repository<Contributor>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(DictionariesDbContextName));

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
            this.Bind<ICommandProcessor>().To<CommandProcessor>().InSingletonScope().Intercept().With<CommandProcessorInterceptor>();
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();
            this.Bind<ILogger>().To<ExceptionLogger>().WhenInjectedInto<IEngine>().InSingletonScope();

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
            this.Bind<IWordsImporterFactory>().ToFactory().InSingletonScope();
            this.Bind<IWordsImporterProvider>().ToMethod(context =>
            {
                IList<IParameter> parameters = context.Parameters.ToList();
                string importerName = (string)parameters[0].GetValue(context, null);

                var importer = context.Kernel.Get<IWordsImporterProvider>(importerName);

                return importer;
            }).NamedLikeFactoryMethod((IWordsImporterFactory factory) => factory.GetImporter(null));

            // commands factory
            this.Bind<ICommandFactory>().ToFactory().InSingletonScope();
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
            var createDictionaryBinding = this.Bind<ICommand>().To<CreateDictionaryCommand>().Named(CreateDictionaryCommandName);
            var addWordToDictionaryBinding = this.Bind<ICommand>().To<AddWordToDictionaryCommand>().Named(AddWordToDictionaryCommandName);

            // delete
            var deleteDictionaryBinding = this.Bind<ICommand>().To<DeleteDictionaryCommand>().Named(DeleteDictionaryCommandName);
            var deleteWordBinding = this.Bind<ICommand>().To<DeleteWordCommand>().Named(DeleteWordCommandName);

            // read
            var generateReportBinding = this.Bind<ICommand>().To<GenerateUsersReportCommand>().Named(GeneratePdfReportCommandName);
            var listWordInfoBinding = this.Bind<ICommand>().To<ListWordInformationCommand>().Named(ListWordInformationCommandName);
            var listDictionaryBinding = this.Bind<ICommand>().To<ListDictionaryCommand>().Named(ListDictionaryCommandName);
            var listBadgesBinding = this.Bind<ICommand>().To<ListBadgesCommand>().Named(ListUserBadgesCommandName);

            // update
            var updateWordBinding = this.Bind<ICommand>().To<UpdateWordCommand>().Named(UpdateWordCommandName);
            var importWordsBinding = this.Bind<ICommand>().To<ImportWordsFromFileCommand>().Named(ImportWordsFromFileCommandName);

            // common
            this.Bind<ICommand>().To<ContributorsCommand>().Named(ContributorsCommandName);
            this.Bind<ICommand>().To<RegisterCommand>().Named(RegisterUserCommandName);
            this.Bind<ICommand>().To<LoginCommand>().Named(LoginUserCommandName);
            this.Bind<ICommand>().To<LogoutCommand>().Named(LogoutCommandName);
            this.Bind<ICommand>().To<HelpCommand>().Named(HelpCommandName);
            this.Bind<ICommand>().To<ClearCommand>().Named(ClearCommandName);

            // interceptor bindings
            this.Bind<UserLoggerInterceptor>().ToSelf();
            this.Bind<ILogger>().To<UserLogger>().WhenInjectedInto<UserLoggerInterceptor>();

            this.Bind<IConfigurationProvider>().To<ConfigurationProvider>();
            var configurationProvider = this.Kernel.Get<IConfigurationProvider>();

            if (!configurationProvider.IsTestEnvironment())
            {
                // command interceptions
                // create
                createDictionaryBinding.Intercept().With<UserAuthenticatorInterceptor>();
                createDictionaryBinding.Intercept().With<UserLoggerInterceptor>();
                createDictionaryBinding.Intercept().With<UserContributionsInterceptor>();

                addWordToDictionaryBinding.Intercept().With<UserAuthenticatorInterceptor>();
                addWordToDictionaryBinding.Intercept().With<UserLoggerInterceptor>();
                addWordToDictionaryBinding.Intercept().With<UserContributionsInterceptor>();

                // delete
                deleteDictionaryBinding.Intercept().With<UserAuthenticatorInterceptor>();
                deleteDictionaryBinding.Intercept().With<UserLoggerInterceptor>();

                deleteWordBinding.Intercept().With<UserAuthenticatorInterceptor>();
                deleteWordBinding.Intercept().With<UserLoggerInterceptor>();

                // read
                generateReportBinding.Intercept().With<UserAuthenticatorInterceptor>();
                generateReportBinding.Intercept().With<UserLoggerInterceptor>();

                listDictionaryBinding.Intercept().With<UserAuthenticatorInterceptor>();
                listDictionaryBinding.Intercept().With<UserLoggerInterceptor>();

                listWordInfoBinding.Intercept().With<UserAuthenticatorInterceptor>();
                listWordInfoBinding.Intercept().With<UserLoggerInterceptor>();

                listBadgesBinding.Intercept().With<UserAuthenticatorInterceptor>();
                listBadgesBinding.Intercept().With<UserLoggerInterceptor>();

                // update
                updateWordBinding.Intercept().With<UserAuthenticatorInterceptor>();
                updateWordBinding.Intercept().With<UserLoggerInterceptor>();
                updateWordBinding.Intercept().With<UserContributionsInterceptor>();

                importWordsBinding.Intercept().With<UserAuthenticatorInterceptor>();
                importWordsBinding.Intercept().With<UserLoggerInterceptor>();
                importWordsBinding.Intercept().With<UserContributionsInterceptor>();
            }
        }
    }
}