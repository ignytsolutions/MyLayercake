using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLayercake.Core.DataAccess {
    public interface IRepositoryManyAsync<T> where T : IEntity {
        Task InsertManyAsync(IEnumerable<T> entities);
        Task UpdateManyAsync(IEnumerable<T> entities);
        Task DeleteManyAsync(IEnumerable<T> entities);
    }
}
