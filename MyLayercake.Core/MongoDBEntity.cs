using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.Core {
    public abstract class MongoDBEntity : IEntity {
        private DateTime _created;

        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Oid { get; set; }

        public DateTime Created {
            get {
                if (_created == null)
                    _created = DateTime.Now;
                return _created;
            }
            set {
                _created = value;
            }
        }
    }
}
