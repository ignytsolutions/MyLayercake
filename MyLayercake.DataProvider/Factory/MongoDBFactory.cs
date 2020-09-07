using MyLayercake.Core;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public class MongoDBFactory<TEntity> : ProviderFactory<TEntity> where TEntity : IMongoDBEntity {
        private IDatabaseSettings IDatabaseSettings { get; set; }

        public MongoDBFactory(IDatabaseSettings IDatabaseSettings) {
            this.IDatabaseSettings = IDatabaseSettings;
        }

        public override DataProvider<TEntity> GetDataProvider() {
            return new MongoDBDataProvider<TEntity>(this.IDatabaseSettings);
        }
    }
}
