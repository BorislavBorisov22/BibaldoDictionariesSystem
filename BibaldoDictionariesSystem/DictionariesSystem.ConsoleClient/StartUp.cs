using DictionariesSystem.ConsoleClient.Container;
using DictionariesSystem.Contracts.Core;
using Ninject;

namespace DictionariesSystem.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {    
            var module = new DictionariesSystemModule();
            var kernel = new StandardKernel(module);
            var engine = kernel.Get<IEngine>();
            engine.Start();
        }
    }
}