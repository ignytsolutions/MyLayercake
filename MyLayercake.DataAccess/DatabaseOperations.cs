using System.Data.Common;
using System.Threading.Tasks;

namespace MyLayercake.DataAccess {
    /// <summary>
    /// Abstraction for Database Operations
    /// </summary>
    /// <implements>
    /// Implements IDatabaseOperations
    /// </implements>
    public abstract class DatabaseOperations : IDatabaseOperations {
		#region Properties
		public DbProviderFactory DbProviderFactory { get; set; }
		public DbConnection DbConnection { get; set; }
		public DbCommand DbCommand { get; set; }
		public DbTransaction DbTransaction { get; set; }
		#endregion

		public DatabaseOperations(DbProviderFactory dbProviderFactory) {
			this.DbProviderFactory = dbProviderFactory;
			this.DbConnection = this.DbProviderFactory.CreateConnection();
            this.DbConnection.Open();
		}

        #region Abstract
        public abstract void Select<T>() where T : class, new();

		public abstract void SelectByID<T>(object ID) where T : class, new();

		public abstract Task SelectByIDAsync<T>(object ID) where T : class, new();

		public abstract void Insert<T>(T objectToInsert) where T : class, new();

		public abstract Task InsertAsync<T>(T objectToInsert) where T : class, new();

		public abstract void Update<T>(T objectToUpdate) where T : class, new();

		public abstract Task UpdateAsync<T>(T objectToUpdate) where T : class, new();

		public abstract void Delete<T>(T objectToUpdate) where T : class, new();

		public abstract Task DeleteAsync<T>(T objectToUpdate) where T : class, new();
		#endregion

		public void Dispose() {
			if (this.DbConnection.State == System.Data.ConnectionState.Open)
				this.DbConnection.Close();
		}
	}
}
