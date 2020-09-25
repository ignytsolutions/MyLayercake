using System.Runtime.CompilerServices;

namespace MyLayercake.DataProvider {
    public class DatabaseSettings : IDatabaseSettings {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string Provider { get; set; }

        public DatabaseSettings(string DatabaseName, string ConnectionString, string Provider) {
            this.DatabaseName = DatabaseName;
            this.ConnectionString = ConnectionString;
            this.Provider = Provider;
        }
    }
}
