using System;
using Ninject.Extensions.Interception;
using DictionariesSystem.Contracts.Core.Providers;
using Bytes2you.Validation;

namespace DictionariesSystem.ConsoleClient.Interceptors
{
    public class UserAuthenticatorInterceptor : IInterceptor
    {
        private readonly IUserProvider userProvider;

        public UserAuthenticatorInterceptor(IUserProvider userProvider)
        {
            Guard.WhenArgument(userProvider, "userProvider").IsNull().Throw();

            this.userProvider = userProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!this.userProvider.IsLogged)
            {
                invocation.ReturnValue = $"You must be logged in order to run {invocation.Request.Target.GetType().Name}!";
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
