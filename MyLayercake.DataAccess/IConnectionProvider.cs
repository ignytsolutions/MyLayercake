namespace MyLayercake.DataAccess {
    public interface IConnectionProvider {
		string Provider { get; set; }

		string ConnectionString { get; set; }

		DatabaseOperations DatabaseOperations { get; set; }
    }
}
