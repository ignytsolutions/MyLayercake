using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyLayercake.Core.Attributes;
using System;

namespace MyLayercake.Core {
    public interface IEntity { }

    public interface IEntity<TKey> : IEntity {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        TKey Oid { get; set; }

        DateTime Created { get; set; }
    }
}
