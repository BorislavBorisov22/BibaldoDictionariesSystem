using System;
using Ninject.Extensions.Interception;
using DictionariesSystem.Framework.Core.Exceptions;
using Ninject;

namespace DictionariesSystem.ConsoleClient.Interceptors
{
    public class CommandProcessorInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (UserAuthenticationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new InvalidCommandException(ex.Message, ex);
            }
        }
    }
}
