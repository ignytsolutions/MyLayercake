using MyLayercake.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public sealed class DBDataProvider : DataProvider {
        private readonly IDatabaseContextFactory factory;

        public DBDataProvider(IDatabaseSettings DatabaseSettings) : base(DatabaseSettings) {
            this.factory = new DatabaseContextFactory(new ConnectionProvider(DatabaseSettings.Provider, DatabaseSettings.ConnectionString,DatabaseSettings.OpenConnection));
        }

        public override void DeleteById<TEntity>(TEntity entity) {
            new RepositoryBase<TEntity>(factory.Context).Delete(entity);
        }

        public override Task DeleteByIdAsync<TEntity>(TEntity entity) {
            return new RepositoryBase<TEntity>(factory.Context).DeleteAsync(entity);
        }

        public override void DeleteMany<TEntity>(IEnumerable<TEntity> entities) {
            new RepositoryBase<TEntity>(factory.Context).DeleteMany(entities);
        }

        public override Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) {
            return new RepositoryBase<TEntity>(factory.Context).DeleteManyAsync(entities);
        }

        public override IEnumerable<TEntity> FilterBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> FilterByAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override TEntity FindById<TEntity>(object oid) {
            return new RepositoryBase<TEntity>(factory.Context).GetById(oid);
        }

        public override Task<TEntity> FindByIdAsync<TEntity>(object oid) {
            return new RepositoryBase<TEntity>(factory.Context).GetByIdAsync(oid);
        }

        public override TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override void InsertMany<TEntity>(IEnumerable<TEntity> entities) {
            new RepositoryBase<TEntity>(factory.Context).InsertMany(entities);
        }

        public override Task InsertManyAsync<TEntity>(IEnumerable<TEntity> entities) {
            return new RepositoryBase<TEntity>(factory.Context).InsertManyAsync(entities);
        }

        public override void InsertOne<TEntity>(TEntity entity) {
            new RepositoryBase<TEntity>(factory.Context).Insert(entity);
        }

        public override Task InsertOneAsync<TEntity>(TEntity entity) {
            return new RepositoryBase<TEntity>(factory.Context).InsertAsync(entity);
        }

        public override IEnumerable<TEntity> SelectAll<TEntity>() {
            return new RepositoryBase<TEntity>(factory.Context).GetAll();
        }

        public override Task<IEnumerable<TEntity>> SelectAllAsync<TEntity>() {
            return new RepositoryBase<TEntity>(factory.Context).GetAllAsync();
        }

        public override void UpdateMany<TEntity>(IEnumerable<TEntity> entities) {
            new RepositoryBase<TEntity>(factory.Context).UpdateMany(entities);
        }

        public override Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) {
            return new RepositoryBase<TEntity>(factory.Context).UpdateManyAsync(entities);
        }

        public override void UpdateOne<TEntity>(TEntity entity) {
            new RepositoryBase<TEntity>(factory.Context).Update(entity);
        }

        public override Task UpdateOneAsync<TEntity>(TEntity entity) {
            return new RepositoryBase<TEntity>(factory.Context).UpdateAsync(entity);
        }

        public override void Dispose() {
            if (factory != null)
                factory.Dispose();
        }
    }
}
