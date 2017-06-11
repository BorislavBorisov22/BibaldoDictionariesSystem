using System;

namespace DictionariesSystem.Framework.Core.Exceptions
{
    public class InvalidCommandException : ApplicationException
    {
        public InvalidCommandException(string message, Exception exception) 
            : base(message, exception)
        {
        }
    }
}
