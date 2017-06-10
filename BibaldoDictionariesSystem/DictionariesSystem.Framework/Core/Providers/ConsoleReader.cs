using System;
using DictionariesSystem.Contracts.Core.Providers;

namespace DictionariesSystem.Framework.Core.Providers
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
