using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLayercake.DataAccess {

    /// <summary>
    /// A class that wraps your database table in Dynamic Funtime
    /// </summary>
    public abstract class DatabaseDataAccess : DynamicObject, IDatabaseOperations {
		private readonly DbProviderFactory _factory;
		private readonly string _connectionString;
		private readonly DatabaseOperations _databaseOperations;

		public DatabaseDataAccess(IConnectionProvider connectionProvider) {
			this._factory = DbProviderFactories.GetFactory(connectionProvider.Provider);
            this._connectionString = connectionProvider.ConnectionString;
            this._databaseOperations = connectionProvider.DatabaseOperations;
		}

        public void Delete<T>(T objectToDelete) where T : class, new() {
            this._databaseOperations.Delete<T>(objectToDelete);
        }

        public Task DeleteAsync<T>(T objectToDelete) where T : class, new() {
            this._databaseOperations.DeleteAsync<T>(objectToDelete);
        }

        public void Insert<T>(T objectToInsert) where T : class, new() {
            this._databaseOperations.Insert<T>(objectToInsert);
        }

        public Task InsertAsync<T>(T objectToInsert) where T : class, new() {
            this._databaseOperations.Delete<T>(objectToInsert);
        }

        public void Select<T>() where T : class, new() {
            this._databaseOperations.Delete<T>(objectToUpdate);
        }

        public void SelectByID<T>(object ID) where T : class, new() {
            this._databaseOperations.Delete<T>(objectToUpdate);
        }

        public Task SelectByIDAsync<T>(object ID) where T : class, new() {
            this._databaseOperations.Delete<T>(objectToUpdate);
        }

        public void Update<T>(T objectToUpdate) where T : class, new() {
            this._databaseOperations.Delete<T>(objectToUpdate);
        }

        public Task UpdateAsync<T>(T objectToUpdate) where T : class, new() {
            this._databaseOperations.Delete<T>(objectToUpdate);
        }

        public void Dispose() {
            this._databaseOperations.Dispose();
        }
    }
}
