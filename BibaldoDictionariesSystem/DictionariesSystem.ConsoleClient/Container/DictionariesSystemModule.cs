using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Contracts.Loaders;
using DictionariesSystem.Data.Common;
using DictionariesSystem.Framework.Core;
using DictionariesSystem.Framework.Core.Factories;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Framework.Loaders;
using Ninject.Modules;

namespace DictionariesSystem.ConsoleClient.Container
{
    public class DictionariesSystemModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEngine>().To<Engine>().InSingletonScope();

            this.Bind<ILogger>().To<ExceptionLogger>().WhenInjectedInto<IEngine>();
            this.Bind<ICommandProcessor>().To<CommandProcessor>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>().InSingletonScope();
            this.Bind<IWriter>().To<ConsoleWriter>().InSingletonScope();

            this.Bind<IUserProvider>().To<UserProvider>().InSingletonScope();
            this.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();

            this.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            this.Bind<IPdfReporterProvider>().To<PdfReporterProvider>();
            //this.Bind<IWordsImporterProvider>().To<XmlWordsImporterProvider>();    bobi
            //this.Bind<IWordsImporterProvider>().To<JsonWordsImporterProvider>();   fix :)
            
            this.Bind<IDictionariesFactory>().To<DictionariesFactory>().InSingletonScope();
            this.Bind<ILogsFactory>().To<LogsFactory>().InSingletonScope();
            this.Bind<IUserFactory>().To<UserFactory>().InSingletonScope();

            //this.Bind<ICommandFactory>().ToFactory().InSingletonScope();

            // TODO: repositories and commands bindings
        }
    }
}
