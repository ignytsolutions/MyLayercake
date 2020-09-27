using MyLayercake.DataProvider.Factory;
using System;

namespace MyLayercake.DataProvider.UnitOfWork {
    public interface IUnitOfWork : IDisposable {
        IDBContext Context { get; }

        void Commit();
    }
}
