using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DictionariesSystem.Contracts.Data
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> All(Expression<Func<T, bool>> filterExpression);

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);
    }
}
