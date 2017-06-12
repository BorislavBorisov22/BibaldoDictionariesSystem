using Bytes2you.Validation;
using DictionariesSystem.Contracts.Data;
using System.Collections.Generic;
using System.Data.Entity;

namespace DictionariesSystem.Data.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEnumerable<DbContext> contexts;

        public UnitOfWork(IEnumerable<DbContext> contexts)
        {
            Guard.WhenArgument(contexts, "contexts").IsNull().Throw();

            this.contexts = contexts;
        }

        public void SaveChanges()
        {
            foreach (var context in this.contexts)
            {
                context.SaveChanges();
            }
        }
    }
}
