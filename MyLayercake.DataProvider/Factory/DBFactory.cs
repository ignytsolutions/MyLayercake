using MyLayercake.Core;
using System;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public class DBFactory : ProviderFactory {
        private IDatabaseSettings IDatabaseSettings { get; set; }

        public DBFactory(IDatabaseSettings IDatabaseSettings) {
            this.IDatabaseSettings = IDatabaseSettings;
        }

        public override DataProvider GetDataProvider(){
            return new DBDataProvider(this.IDatabaseSettings);
        }
    }
}
