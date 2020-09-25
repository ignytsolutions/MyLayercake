namespace MyLayercake.DataAccess {
    public class ConfigurationBasedConnectionProvider : IConnectionProvider {
		public string Provider { get; set; }

		public string ConnectionString { get; set; }

        public DatabaseOperations DatabaseOperations { get; set; }
    }
}
