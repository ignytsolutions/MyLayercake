using System;
using System.Data;

namespace MyLayercake.Core.DataAccess {
    public interface IUnitOfWork : IDisposable {
        IDatabaseContext Context { get; }
        IDbTransaction BeginTransaction();
        void Commit();
    }
}
