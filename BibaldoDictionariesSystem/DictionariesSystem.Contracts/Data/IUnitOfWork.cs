using System;

namespace DictionariesSystem.Contracts.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
