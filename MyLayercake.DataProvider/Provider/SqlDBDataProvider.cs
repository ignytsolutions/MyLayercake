using MyLayercake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public class SqlDBDataProvider<TEntity> : DataProvider<TEntity> where TEntity : ISqlDBEntity {
        public SqlDBDataProvider(IDatabaseSettings DatabaseSettings) : base(DatabaseSettings) {
        }

        public override IQueryable<TEntity> AsQueryable() {
            throw new NotImplementedException();
        }

        public override void DeleteById(string oid) {
            throw new NotImplementedException();
        }

        public override Task DeleteByIdAsync(string oid) {
            throw new NotImplementedException();
        }

        public override void DeleteMany(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override TEntity FindById(string oid) {
            throw new NotImplementedException();
        }

        public override Task<TEntity> FindByIdAsync(string oid) {
            throw new NotImplementedException();
        }

        public override TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression) {
            throw new NotImplementedException();
        }

        public override void InsertMany(IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
        }

        public override Task InsertManyAsync(IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
        }

        public override void InsertOne(TEntity entity) {
            throw new NotImplementedException();
        }

        public override Task InsertOneAsync(TEntity entity) {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> SelectAll() {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TEntity>> SelectAllAsync() {
            throw new NotImplementedException();
        }

        public override void UpdateMany(IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
        }

        public override Task UpdateManyAsync(IEnumerable<TEntity> entities) {
            throw new NotImplementedException();
        }

        public override void UpdateOne(TEntity entity) {
            throw new NotImplementedException();
        }

        public override Task UpdateOneAsync(TEntity entity) {
            throw new NotImplementedException();
        }
    }
}
