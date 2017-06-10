using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Contracts.Core.Factories;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Framework.Core;
using DictionariesSystem.Framework.Core.Factories;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Framework.Core.Providers.Loggers;
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

            this.Bind<IDateProvider>().To<DateProvider>();
            this.Bind<IDictionariesFactory>().To<DictionariesFactory>();
            this.Bind<ILogsFactory>().To<LogsFactory>();
            this.Bind<IUserFactory>().To<UserFactory>();

            //this.Bind<ICommandFactory>().ToFactory().InSingletonScope();
        }
    }
}
