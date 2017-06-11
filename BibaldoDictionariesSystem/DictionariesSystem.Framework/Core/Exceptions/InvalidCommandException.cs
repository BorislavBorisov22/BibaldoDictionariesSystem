using System;

namespace DictionariesSystem.Framework.Core.Exceptions
{
    public class InvalidCommandException : ApplicationException
    {
        public InvalidCommandException(string message) 
            : base(message)
        {
        }
    }
}
