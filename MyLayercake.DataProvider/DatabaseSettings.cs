using System.Runtime.CompilerServices;

namespace MyLayercake.DataProvider {
    public class DatabaseSettings : IDatabaseSettings {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string Provider { get; set; }
        public bool OpenConnection { get; set; }

        public DatabaseSettings(string connectionString, string provider) {
            this.ConnectionString = connectionString;
            this.Provider = provider;
        }

        public DatabaseSettings(string connectionString, string provider, bool openConnection) {
            this.ConnectionString = connectionString;
            this.Provider = provider;
            this.OpenConnection = openConnection;
        }

        public DatabaseSettings(string databaseName, string connectionString, string provider) {
            this.DatabaseName = databaseName;
            this.ConnectionString = connectionString;
            this.Provider = provider;
        }

        public DatabaseSettings(string databaseName, string connectionString, string provider, bool openConnection) {
            this.DatabaseName = databaseName;
            this.ConnectionString = connectionString;
            this.Provider = provider;
            this.OpenConnection = openConnection;
        }
    }
}
