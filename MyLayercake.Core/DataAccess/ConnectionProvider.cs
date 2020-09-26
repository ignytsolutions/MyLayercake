namespace MyLayercake.Core.DataAccess {
    public class ConnectionProvider : IConnectionProvider {
        public string Provider { get; set; }
        public string ConnectionString { get; set; }
        public bool OpenConnection { get; set; }

        public ConnectionProvider(string provider, string connectionString) {
            Provider = provider;
            ConnectionString = connectionString;
        }

        public ConnectionProvider(string provider, string connectionString, bool openConnection) {
            this.Provider = provider;
            this.ConnectionString = connectionString;
            this.OpenConnection = openConnection;
        }
    }
}
