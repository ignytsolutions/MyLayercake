using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyLayercake.Core.Attributes;

namespace MyLayercake.Core {
    public interface IMongoDBEntity : IEntity {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Oid { get; set; }
    }
}
