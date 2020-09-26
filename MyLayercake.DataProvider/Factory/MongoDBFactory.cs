using MongoDB.Bson;
using MyLayercake.Core;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public class MongoDBFactory : ProviderFactory {
        private IDatabaseSettings IDatabaseSettings { get; set; }

        public MongoDBFactory(IDatabaseSettings IDatabaseSettings) {
            this.IDatabaseSettings = IDatabaseSettings;
        }

        public override DataProvider GetDataProvider() {
            return new MongoDBDataProvider(this.IDatabaseSettings);
        }
    }
}
