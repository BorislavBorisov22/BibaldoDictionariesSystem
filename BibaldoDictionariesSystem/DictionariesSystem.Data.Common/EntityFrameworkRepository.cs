using System;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Data;

namespace DictionariesSystem.Data.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Add(T entity)
        {
            
        }

        public IEnumerable<T> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }
    }
}
