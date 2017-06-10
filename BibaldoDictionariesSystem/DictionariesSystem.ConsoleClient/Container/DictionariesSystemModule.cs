using System;
using Ninject.Modules;
using DictionariesSystem.Contracts.Core.Providers;
using DictionariesSystem.Contracts.Data;
using DictionariesSystem.Data.Common;
using DictionariesSystem.Models.Logs;
using DictionariesSystem.Contracts.Core;
using DictionariesSystem.Framework.Core;
using DictionariesSystem.Framework.Core.Providers.Loggers;
using DictionariesSystem.Framework.Core.Providers;
using DictionariesSystem.Contracts.Core.Factories;
using Ninject.Extensions.Factory;

namespace DictionariesSystem.ConsoleClient.Container
{
    public class DictionariesSystemModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IEngine>().To<Engine>().InSingletonScope();

            this.Bind<ILogger>().To<ExceptionLogger>().WhenInjectedInto<IEngine>();
            this.Bind<ICommandProcessor>().To<CommandProcessor>().InSingletonScope();
            this.Bind<IReader>().To<ConsoleReader>();
            this.Bind<IWriter>().To<ConsoleWriter>();

            this.Bind<IDateProvider>().To<DateProvider>();

            //this.Bind<ICommandFactory>().ToFactory().InSingletonScope();
        }
    }
}
