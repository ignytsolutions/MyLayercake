using System.Data;
using System.Data.Common;

namespace MyLayercake.Core.DataAccess {
    public class DatabaseContext : IDatabaseContext {
        private IDbConnection _connection;
        private readonly IConnectionProvider _connectionProvider;
        private DbProviderFactory _dbProviderFactory;

        public DatabaseContext(IConnectionProvider connectionProvider) {
            _connectionProvider = connectionProvider;
        }

        public IDbConnection Connection {
            get {
                if (_connection == null) {
                    _connection = this._dbProviderFactory.CreateConnection();
                    _connection.ConnectionString = this._connectionProvider.ConnectionString;
                }

                if (_connection.State != ConnectionState.Open) {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public DbProviderFactory DbProviderFactory {
            get {
                if (_dbProviderFactory == null) {
                    _dbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider.Provider);
                }

                return _dbProviderFactory;
            }
        }

        public void Dispose() {
            if (this._connection.State == ConnectionState.Open) {
                this._connection.Close();
            }
        }
    }
}
