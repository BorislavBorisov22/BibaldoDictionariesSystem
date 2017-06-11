using System;

namespace DictionariesSystem.Framework.Core.Exceptions
{
    public class UserAuthenticationException : ApplicationException
    {
        public UserAuthenticationException(string message)
            : base(message)
        {
        }
    }
}
