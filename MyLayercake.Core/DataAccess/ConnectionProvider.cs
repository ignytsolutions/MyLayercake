namespace MyLayercake.Core.DataAccess {
    public class ConnectionProvider : IConnectionProvider {
        public string Provider { get; set; }
        public string ConnectionString { get; set; }

        public ConnectionProvider(string provider, string connectionString) {
            Provider = provider;
            ConnectionString = connectionString;
        }
    }
}
