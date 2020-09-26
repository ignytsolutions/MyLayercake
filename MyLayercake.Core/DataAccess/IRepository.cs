using System;
using System.Collections.Generic;

namespace MyLayercake.Core.DataAccess {

    public interface IRepository<T> where T : IEntity {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(object Oid);
        IEnumerable<T> GetAll();
    }
}
