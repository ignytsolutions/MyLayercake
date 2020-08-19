namespace MyLayercake.DataProvider {
    public class DatabaseSettings : IDatabaseSettings {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

        public DatabaseSettings(string DatabaseName, string ConnectionString) {
            this.DatabaseName = DatabaseName;
            this.ConnectionString = ConnectionString;
        }
    }
}
