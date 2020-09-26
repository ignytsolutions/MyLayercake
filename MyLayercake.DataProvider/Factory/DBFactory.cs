using MyLayercake.Core;
using System;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public class DBFactory<TEntity> : ProviderFactory<TEntity> where TEntity : DBEntity, new() {
        private IDatabaseSettings IDatabaseSettings { get; set; }

        public DBFactory(IDatabaseSettings IDatabaseSettings) {
            this.IDatabaseSettings = IDatabaseSettings;
        }

        public override DataProvider<TEntity> GetDataProvider() {
            return new DBDataProvider<TEntity>(this.IDatabaseSettings);
        }
    }
}
