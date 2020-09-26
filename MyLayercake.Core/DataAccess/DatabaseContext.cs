using System.Data;
using System.Data.Common;

namespace MyLayercake.Core.DataAccess {
    public class DatabaseContext : IDatabaseContext {
        private IDbConnection _connection;
        private readonly IConnectionProvider _connectionProvider;

        public IDbConnection Connection {
            get {
                return this._connection;
            }
        }

        public DbProviderFactory DbProviderFactory { get; private set; }


        public DatabaseContext(IConnectionProvider connectionProvider) {
            this._connectionProvider = connectionProvider;

            this.CreateFactory();

            if (connectionProvider.OpenConnection)
                this.OpenConnection();
        }

        private void CreateFactory() {
            if (DbProviderFactory == null) {
                // TODO: Register Factory based on invariantname
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

                this.DbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider.Provider);
            }
        }

        public void OpenConnection() {
            if (_connection == null) {
                this._connection = this.DbProviderFactory.CreateConnection();
                this._connection.ConnectionString = this._connectionProvider.ConnectionString;
            }

            if (this._connection.State != ConnectionState.Open)
                this._connection.Open();
        }

        public void CloseConnection() {
            if (this._connection.State == ConnectionState.Open)
                this._connection.Close();
        }

        public void Dispose() {
            if (this._connection.State == ConnectionState.Open)
                this._connection.Close();
        }
    }
}
