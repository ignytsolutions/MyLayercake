using MyLayercake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public abstract class DataProvider<TEntity> where TEntity : IEntity {
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

        public abstract TEntity FindById(string oid);

        public abstract Task<TEntity> FindByIdAsync(string oid);

        public abstract void InsertOne(TEntity entity);

        public abstract Task InsertOneAsync(TEntity entity);

        public abstract void InsertMany(IEnumerable<TEntity> entities);

        public abstract Task InsertManyAsync(IEnumerable<TEntity> entities);

        public abstract void UpdateOne(TEntity entity);

        public abstract Task UpdateOneAsync(TEntity entity);

        public abstract void UpdateMany(IEnumerable<TEntity> entities);

        public abstract Task UpdateManyAsync(IEnumerable<TEntity> entities);

        public abstract void DeleteById(string oid);

        public abstract Task DeleteByIdAsync(string oid);

        public abstract void DeleteMany(Expression<Func<TEntity, bool>> filterExpression);

        public abstract Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}
