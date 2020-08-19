using System;
using MyLayercake.BusinessObjects.Datalayer.Provider;

namespace MyLayercake.BusinessObjects.Logic {
    public sealed class Repository<T> : RepositoryBase<T>, IDisposable where T : IEntity, new() {
        public Repository(Connection ConnectionObject) : base(ConnectionObject) { }
        public Repository() : base() { }
    }
}