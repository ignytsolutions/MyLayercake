using System;
using System.Collections.Generic;

namespace MyLayercake.Core.DataAccess {
    public interface IRepositoryMany<T> where T : IEntity<Guid> {
        void InsertMany(IEnumerable<T> entities);
        void UpdateMany(IEnumerable<T> entities);
        void DeleteMany(IEnumerable<T> entities);
    }
}
