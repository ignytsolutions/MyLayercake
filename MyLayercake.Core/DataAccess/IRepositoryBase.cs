using System;

namespace MyLayercake.Core.DataAccess {
    public interface IRepositoryBase<T> : IRepository<T>, IRepositoryAsync<T>, IRepositoryMany<T>, IRepositoryManyAsync<T> where T : IEntity {
        IDatabaseContext Context { get; }
    }
}
