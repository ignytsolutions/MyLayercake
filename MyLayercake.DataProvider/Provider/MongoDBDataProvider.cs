using MongoDB.Driver;
using MyLayercake.DataProvider.Entity;
using MyLayercake.DataProvider.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public class MongoDBDataProvider<TEntity> : DataProvider<TEntity> where TEntity : IMongoDBEntity {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoDBDataProvider(IDatabaseSettings DatabaseSettings) : base(DatabaseSettings) {
            var database = new MongoClient(this.DatabaseSettings.ConnectionString).GetDatabase(this.DatabaseSettings.DatabaseName);

            _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type documentType) {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute),true).FirstOrDefault())?.CollectionName;
        }

        public override IQueryable<TEntity> AsQueryable() {
            return _collection.AsQueryable();
        }

        public override void DeleteById(string oid) {
            var objectId = new ObjectId(oid);
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);

            _collection.FindOneAndDelete(filter);
        }

        public override Task DeleteByIdAsync(string oid) {
            return Task.Run(() => {
                var objectId = new ObjectId(oid);
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);

                _collection.FindOneAndDelete(filter);
            });
        }

        public override void DeleteMany(Expression<Func<TEntity, bool>> filterExpression) {
            _collection.DeleteMany(filterExpression);
        }

        public override Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression) {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        public override IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression) {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public override Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression) {
            return Task.Run(() => _collection.Find(filterExpression).ToEnumerable());
        }

        public override TEntity FindById(string oid) {
            var objectId = new ObjectId(oid);
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);

            return _collection.Find(filter).SingleOrDefault();
        }

        public override Task<TEntity> FindByIdAsync(string oid) {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(oid);
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public override TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression) {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public override Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression) {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public override void InsertMany(IEnumerable<TEntity> entities) {
            _collection.InsertMany(entities);
        }

        public override async Task InsertManyAsync(IEnumerable<TEntity> entities) {
            await _collection.InsertManyAsync(entities);
        }

        public override void InsertOne(TEntity entity) {
            _collection.InsertOne(entity);
        }

        public override Task InsertOneAsync(TEntity entity) {
            return Task.Run(() => _collection.InsertOneAsync(entity));
        }

        public override IEnumerable<TEntity> SelectAll() {
            return _collection.Find(null).ToEnumerable<TEntity>();
        }

        public override Task<IEnumerable<TEntity>> SelectAllAsync() {
            return Task.Run(() => _collection.Find(null).ToEnumerable<TEntity>());
        }

        public override void UpdateMany(IEnumerable<TEntity> entities) {
            foreach (TEntity entity in entities) {
                UpdateOne(entity);
            }
        }

        public override Task UpdateManyAsync(IEnumerable<TEntity> entities) {
            return Task.Run(() => {
                foreach (TEntity entity in entities) {
                    UpdateOneAsync(entity);
                }
            });
        }

        public override void UpdateOne(TEntity entity) {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, entity.Oid);
            _collection.FindOneAndReplace(filter, entity);
        }

        public override Task UpdateOneAsync(TEntity entity) {
            return Task.Run(() => { var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, entity.Oid);
                _collection.FindOneAndReplace(filter, entity); } );
        }
    }
}
