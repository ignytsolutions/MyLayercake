using MyLayercake.Core;
using System;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public class DBContext : IDBContext {
        private readonly IDatabaseSettings databaseSettings;
        private DataProvider dataProvider;

        public IDatabaseSettings IDatabaseSettings {
            get {
                return this.databaseSettings;
            }
        }

        public DataProvider Instance {
            get {
                if (this.dataProvider == null)
                    this.dataProvider = new DBDataProvider(this.databaseSettings);

                return this.dataProvider;
            }
        }

        public DBContext(IDatabaseSettings databaseSettings) {
            this.databaseSettings = databaseSettings;
        }

        public void Dispose() {
            if (this.dataProvider != null) {
                this.dataProvider.Dispose();
                this.dataProvider = null;
            }
        }
    }
}
