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
    public sealed class MongoDBDataProvider : DataProvider {
        private readonly IMongoDatabase _database;

        public MongoDBDataProvider(IDatabaseSettings DatabaseSettings) : base(DatabaseSettings) {
            this._database = new MongoClient(this.DatabaseSettings.ConnectionString).GetDatabase(this.DatabaseSettings.DatabaseName);
        }

        private string GetCollectionName(Type documentType) {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute),true).FirstOrDefault())?.CollectionName;
        }

        public override void DeleteById<TEntity>(TEntity entity) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            var objectId = (entity as MongoDBEntity).Oid;
            var filter = Builders<TEntity>.Filter.Eq(doc => (doc as MongoDBEntity).Oid, objectId);

            collection.FindOneAndDelete<TEntity>(filter);
        }

        public override Task DeleteByIdAsync<TEntity>(TEntity entity) {
            return Task.Run(() => {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

                var objectId = (entity as MongoDBEntity).Oid;
                var filter = Builders<TEntity>.Filter.Eq(doc => (doc as MongoDBEntity).Oid, objectId);

                collection.FindOneAndDelete<TEntity>(filter);
            });
        }

        public override void DeleteMany<TEntity>(IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                DeleteById(entity);
            }
        }

        public override Task DeleteManyAsync<TEntity>(IEnumerable<TEntity> entities) {
            return Task.Run(() => DeleteMany(entities) );
        }

        public override IEnumerable<TEntity> FilterBy<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            return collection.Find(filterExpression).ToEnumerable();
        }

        public override Task<IEnumerable<TEntity>> FilterByAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            return Task.Run(() => {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

                return collection.Find(filterExpression).ToEnumerable();
            });
        }

        public override TEntity FindById<TEntity>(object oid) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            var objectId = new ObjectId(oid.ToString());

            var filter = Builders<TEntity>.Filter.Eq(doc => (doc as MongoDBEntity).Oid, objectId);

            return collection.Find(filter).SingleOrDefault();
        }

        public override Task<TEntity> FindByIdAsync<TEntity>(object oid) {
            return Task.Run(() =>
            {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

                var objectId = new ObjectId(oid.ToString());
                var filter = Builders<TEntity>.Filter.Eq(doc => (doc as MongoDBEntity).Oid, objectId);

                return collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public override TEntity FindOne<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            return collection.Find(filterExpression).FirstOrDefault();
        }

        public override Task<TEntity> FindOneAsync<TEntity>(Expression<Func<TEntity, bool>> filterExpression) {
            return Task.Run(() => {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

                return collection.Find(filterExpression).FirstOrDefaultAsync();
            });
        }

        public override void InsertMany<TEntity>(IEnumerable<TEntity> entities) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            collection.InsertMany(entities);
        }

        public override async Task InsertManyAsync<TEntity>(IEnumerable<TEntity> entities) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            await collection.InsertManyAsync(entities);
        }

        public override void InsertOne<TEntity>(TEntity entity) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            collection.InsertOne(entity);
        }

        public override Task InsertOneAsync<TEntity>(TEntity entity) {
            return Task.Run(() => {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

                collection.InsertOneAsync(entity);
            });
        }

        public override IEnumerable<TEntity> SelectAll<TEntity>() {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

            return collection.Find(null).ToEnumerable<TEntity>();
        }

        public override Task<IEnumerable<TEntity>> SelectAllAsync<TEntity>() {
            return Task.Run(() => {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

                return collection.Find(null).ToEnumerable<TEntity>();
            });
        }

        public override void UpdateMany<TEntity>(IEnumerable<TEntity> entities) {
            foreach (TEntity entity in entities) {
                UpdateOne(entity);
            }
        }

        public override Task UpdateManyAsync<TEntity>(IEnumerable<TEntity> entities) {
            return Task.Run(() => {
                foreach (TEntity entity in entities) {
                    UpdateOneAsync(entity);
                }
            });
        }

        public override void UpdateOne<TEntity>(TEntity entity) {
            var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
            var filter = Builders<TEntity>.Filter.Eq(doc => (doc as MongoDBEntity).Oid, (entity as MongoDBEntity).Oid);

            collection.FindOneAndReplace(filter, entity);
        }

        public override Task UpdateOneAsync<TEntity>(TEntity entity) {
            return Task.Run(() => {
                var collection = this._database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
                var filter = Builders<TEntity>.Filter.Eq(doc => (doc as MongoDBEntity).Oid, (entity as MongoDBEntity).Oid);

                collection.FindOneAndReplace(filter, entity);
            });
        }

        // Handled Automatically by the MongoDB CLient
        public override void Dispose() { }
    }
}
