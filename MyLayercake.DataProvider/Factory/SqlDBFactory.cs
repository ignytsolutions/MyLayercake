using MyLayercake.Core;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public class SqlDBFactory<TEntity> : ProviderFactory<TEntity> where TEntity : ISqlDBEntity {
        private IDatabaseSettings IDatabaseSettings { get; set; }

        public SqlDBFactory(IDatabaseSettings IDatabaseSettings) {
            this.IDatabaseSettings = IDatabaseSettings;
        }

        public override DataProvider<TEntity> GetDataProvider() {
            return new SqlDBDataProvider<TEntity>(this.IDatabaseSettings);
        }
    }
}
