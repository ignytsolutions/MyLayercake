using MyLayercake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public abstract class DataProvider : IDisposable {
        protected IDatabaseSettings DatabaseSettings { get; set; }

        protected DataProvider(IDatabaseSettings DatabaseSettings) {
            this.DatabaseSettings = DatabaseSettings;
        }

        public abstract IEnumerable<TEntity> FilterBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : IEntity, new();

        public abstract TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : IEntity, new();

        public abstract Task<IEnumerable<TEntity>> FilterByAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : IEntity, new();

        public abstract Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) where TEntity : IEntity, new();

        public abstract IEnumerable<TEntity> SelectAll<TEntity>() where TEntity : IEntity, new();

        public abstract Task<IEnumerable<TEntity>> SelectAllAsync<TEntity>() where TEntity : IEntity, new();

        public abstract TEntity FindById<TEntity>(object oid) where TEntity : IEntity, new();

        public abstract Task<TEntity> FindByIdAsync<TEntity>(object oid) where TEntity : IEntity, new();

        public abstract void InsertOne<TEntity>(TEntity entity) where TEntity : IEntity, new();

        public abstract Task InsertOneAsync<TEntity>(TEntity entity) where TEntity : IEntity, new();

        public abstract void InsertMany<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new();

        public abstract Task InsertManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new();

        public abstract void UpdateOne<TEntity>(TEntity entity) where TEntity : IEntity, new();

        public abstract Task UpdateOneAsync<TEntity>(TEntity entity) where TEntity : IEntity, new();

        public abstract void UpdateMany<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new();

        public abstract Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new();

        public abstract void DeleteById<TEntity>(TEntity entity) where TEntity : IEntity, new();

        public abstract Task DeleteByIdAsync<TEntity>(TEntity entity) where TEntity : IEntity, new();

        public abstract void DeleteMany<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new();

        public abstract Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : IEntity, new();

        public abstract void Dispose();
    }
}
