using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MyLayercake.Core.Attributes;
using MyLayercake.Core;

namespace MyLayercake.DataProvider {
    // Template Design Pattern
    public class MongoDBDataProvider<TEntity> : DataProvider<TEntity> where TEntity : IEntity<ObjectId>, new() {
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

        public override void DeleteById(TEntity entity) {
            var objectId = entity.Oid;
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);

            _collection.FindOneAndDelete(filter);
        }

        public override Task DeleteByIdAsync(TEntity entity) {
            return Task.Run(() => {
                var objectId = entity.Oid;
                var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);

                _collection.FindOneAndDelete(filter);
            });
        }

        public override void DeleteMany(IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                DeleteById(entity);
            }
        }

        public override Task DeleteManyAsync(IEnumerable<TEntity> entities) {
            return Task.Run(() => DeleteMany(entities) );
        }

        public override IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression) {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public override Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression) {
            return Task.Run(() => _collection.Find(filterExpression).ToEnumerable());
        }

        public override TEntity FindById(object oid) {
            var objectId = new ObjectId(oid.ToString());
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Oid, objectId);

            return _collection.Find(filter).SingleOrDefault();
        }

        public override Task<TEntity> FindByIdAsync(object oid) {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(oid.ToString());
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
