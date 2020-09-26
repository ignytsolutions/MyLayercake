using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLayercake.Core.DataAccess {
    public interface IRepositoryAsync<T> where T : IEntity {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(object Oid);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
