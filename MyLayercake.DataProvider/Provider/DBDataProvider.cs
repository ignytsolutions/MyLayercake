using MyLayercake.Core;
using MyLayercake.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public class DBDataProvider<TEntity> : DataProvider<TEntity> where TEntity : DBEntity, new() {
        private readonly IDatabaseContextFactory factory;

        public DBDataProvider(IDatabaseSettings DatabaseSettings) : base(DatabaseSettings) {
            this.factory = new DatabaseContextFactory(new ConnectionProvider(DatabaseSettings.Provider, DatabaseSettings.ConnectionString,DatabaseSettings.OpenConnection));
        }

        public override IQueryable<TEntity> AsQueryable() {
            throw new NotImplementedException();
        }

        public override void DeleteById(TEntity entity) {
            new RepositoryBase<TEntity>(factory.Context).Delete(entity);
        }

        public override Task DeleteByIdAsync(TEntity entity) {
            return new RepositoryBase<TEntity>(factory.Context).DeleteAsync(entity);
        }

        public override void DeleteMany(IEnumerable<TEntity> entities) {
            new RepositoryBase<TEntity>(factory.Context).DeleteMany(entities);
        }

        public override Task DeleteManyAsync(IEnumerable<TEntity> entities) {
            return new RepositoryBase<TEntity>(factory.Context).DeleteManyAsync(entities);
        }

        public override IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override TEntity FindById(object oid) {
            return new RepositoryBase<TEntity>(factory.Context).GetById(oid);
        }

        public override Task<TEntity> FindByIdAsync(object oid) {
            return new RepositoryBase<TEntity>(factory.Context).GetByIdAsync(oid);
        }

        public override TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override void InsertMany(IEnumerable<TEntity> entities) {
            new RepositoryBase<TEntity>(factory.Context).InsertMany(entities);
        }

        public override Task InsertManyAsync(IEnumerable<TEntity> entities) {
            return new RepositoryBase<TEntity>(factory.Context).InsertManyAsync(entities);
        }

        public override void InsertOne(TEntity entity) {
            new RepositoryBase<TEntity>(factory.Context).Insert(entity);
        }

        public override Task InsertOneAsync(TEntity entity) {
            return new RepositoryBase<TEntity>(factory.Context).InsertAsync(entity);
        }

        public override IEnumerable<TEntity> SelectAll() {
            return new RepositoryBase<TEntity>(factory.Context).GetAll();
        }

        public override Task<IEnumerable<TEntity>> SelectAllAsync() {
            return new RepositoryBase<TEntity>(factory.Context).GetAllAsync();
        }

        public override void UpdateMany(IEnumerable<TEntity> entities) {
            new RepositoryBase<TEntity>(factory.Context).UpdateMany(entities);
        }

        public override Task UpdateManyAsync(IEnumerable<TEntity> entities) {
            return new RepositoryBase<TEntity>(factory.Context).UpdateManyAsync(entities);
        }

        public override void UpdateOne(TEntity entity) {
            new RepositoryBase<TEntity>(factory.Context).Update(entity);
        }

        public override Task UpdateOneAsync(TEntity entity) {
            return new RepositoryBase<TEntity>(factory.Context).UpdateAsync(entity);
        }

        public override void Dispose() {
            if (factory != null)
                factory.Dispose();
        }
    }
}
