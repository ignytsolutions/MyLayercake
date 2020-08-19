using System;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BsonCollectionAttribute : Attribute {
        public string CollectionName { get; }

        public BsonCollectionAttribute(string collectionName) {
            CollectionName = collectionName;
        }
    }
}
