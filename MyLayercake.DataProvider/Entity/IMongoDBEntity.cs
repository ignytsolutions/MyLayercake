using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyLayercake.DataProvider.Entity.Attributes.Properties;
using System;

namespace MyLayercake.DataProvider.Entity {
    public interface IMongoDBEntity : IEntity {
        [IsPrimaryKey]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Oid { get; set; }
    }
}
