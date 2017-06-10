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
using DictionariesSystem.Framework.Core.Commands.Read;
using DictionariesSystem.Framework.Core.Factories;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Framework.Loaders;
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

        public const string AddWordToDictionaryCommandName = "AddWordToDictionary";
        public const string GeneratePdfReportCommandName = "GeneratePdfReport";

        private const string LogsDbContextName = "LogsDbContext";
        private const string UsersDbContextName = "UsersDbContext";
        private const string DictionariesDbContextName = "DictionariesDbContext";

        private const string LogsUnitOfWorkName = "LogsUnitOfWork";
        private const string UsersUnitOfWorkName = "UsersUnitOfWork";
        private const string DictionariesUnitOfWorkName = "DictionariesUnitOfWork";

        public override void Load()
        {
            // engine and engine depndencies
            this.Bind<IEngine>().To<Engine>().InSingletonScope();
            this.Bind<ILogger>().To<ExceptionLogger>().WhenInjectedInto<IEngine>();
            this.Bind<ICommandProcessor>().To<CommandProcessor>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();

            this.Bind<IUserProvider>().To<UserProvider>().InSingletonScope();
            this.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();

            // repositories
            this.Bind<DbContext>().To<LogsDbContext>().InSingletonScope().Named(LogsDbContextName);
            this.Bind<DbContext>().To<UsersDbContext>().InSingletonScope().Named(UsersDbContextName);
            this.Bind<DbContext>().To<DictionariesDbContext>().InSingletonScope().Named(DictionariesDbContextName);

            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope().Named(LogsUnitOfWorkName).WithConstructorArgument("context", this.Kernel.Get<DbContext>(LogsDbContextName));
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope().Named(UsersUnitOfWorkName).WithConstructorArgument("context", this.Kernel.Get<DbContext>(UsersDbContextName));
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope().Named(DictionariesUnitOfWorkName).WithConstructorArgument("context", this.Kernel.Get<DbContext>(DictionariesDbContextName));

            this.Bind<IRepository<User>>().To<Repository<User>>()
                .InSingletonScope()
                .WithConstructorArgument("context", this.Kernel.Get<DbContext>(UsersDbContextName));
            
            // models factories
            this.Bind<IDictionariesFactory>().To<DictionariesFactory>().InSingletonScope();
            this.Bind<ILogsFactory>().To<LogsFactory>().InSingletonScope();
            this.Bind<IUserFactory>().To<UserFactory>().InSingletonScope();
            
            // importers and reporters
            this.Bind<IWordsImporterProvider>().To<XmlWordsImporterProvider>().Named(XmlWordsImporterName);
            this.Bind<IWordsImporterProvider>().To<JsonWordsImporterProvider>().Named(JsonWordsImporterName);
            this.Bind<IPdfReporterProvider>().To<PdfReporterProvider>();

            // importers factory
            this.Bind<IWordsImporterProvider>().ToMethod(context =>
            {
                IList<IParameter> parameters = context.Parameters.ToList();
                string importerName = (string)parameters[0].GetValue(context, null);

                var importer = context.Kernel.Get<IWordsImporterProvider>(importerName);

                return importer;
            }).NamedLikeFactoryMethod((IWordsImporterFactory factory) => factory.GetImporter(null));

            // commands
            this.Bind<ICommand>().To<AddWordToDictionaryCommand>().Named(AddWordToDictionaryCommandName);
            this.Bind<ICommand>().To<GeneratePdfReportCommand>().Named(GeneratePdfReportCommandName);

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
        }
    }
}