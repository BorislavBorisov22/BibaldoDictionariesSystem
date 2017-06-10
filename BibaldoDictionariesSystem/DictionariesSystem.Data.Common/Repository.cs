using System;
using System.Collections.Generic;
using DictionariesSystem.Contracts.Data;
using System.Linq.Expressions;
using System.Data.Entity;
using Bytes2you.Validation;
using System.Linq;

namespace DictionariesSystem.Data.Common
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbSet<T> dbSet;
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            Guard.WhenArgument(context, "DbContext").IsNull().Throw();

            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Added;
        }

        public IEnumerable<T> All(Expression<Func<T, bool>> filterExpression)
        {
            var entities = this.dbSet.Where(filterExpression);
            return entities.ToList();
        }

        public void Delete(T entity)
        {
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public T GetById(object id)
        {
            var entity = this.dbSet.Find(id);
            return entity;
        }

        public void Update(T entity)
        {
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
