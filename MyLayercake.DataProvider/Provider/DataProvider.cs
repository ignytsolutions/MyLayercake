using MyLayercake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public abstract class DataProvider<TEntity> : IDisposable where TEntity : IEntity, new() {
        protected IDatabaseSettings DatabaseSettings { get; set; }

        protected DataProvider(IDatabaseSettings DatabaseSettings) {
            this.DatabaseSettings = DatabaseSettings;
        }

        public abstract IQueryable<TEntity> AsQueryable();

        public abstract IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression);

        public abstract TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression);

        public abstract Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression);

        public abstract Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        public abstract IEnumerable<TEntity> SelectAll();

        public abstract Task<IEnumerable<TEntity>> SelectAllAsync();

        public abstract TEntity FindById(object oid);

        public abstract Task<TEntity> FindByIdAsync(object oid);

        public abstract void InsertOne(TEntity entity);

        public abstract Task InsertOneAsync(TEntity entity);

        public abstract void InsertMany(IEnumerable<TEntity> entities);

        public abstract Task InsertManyAsync(IEnumerable<TEntity> entities);

        public abstract void UpdateOne(TEntity entity);

        public abstract Task UpdateOneAsync(TEntity entity);

        public abstract void UpdateMany(IEnumerable<TEntity> entities);

        public abstract Task UpdateManyAsync(IEnumerable<TEntity> entities);

        public abstract void DeleteById(TEntity entity);

        public abstract Task DeleteByIdAsync(TEntity entity);

        public abstract void DeleteMany(IEnumerable<TEntity> entities);

        public abstract Task DeleteManyAsync(IEnumerable<TEntity> entities);

        public abstract void Dispose();
    }
}
