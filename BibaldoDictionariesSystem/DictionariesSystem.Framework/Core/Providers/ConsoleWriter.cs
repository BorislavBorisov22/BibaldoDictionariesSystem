using System;
using DictionariesSystem.Contracts.Core.Providers;

namespace DictionariesSystem.Framework.Core.Providers
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
