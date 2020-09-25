using System;
using System.Threading.Tasks;

namespace MyLayercake.DataAccess {
    /// <summary>
    /// Interface for Database Operations
    /// </summary>
    /// <implements>
    /// Implements IDisposable
    /// </implements>
    public interface IDatabaseOperations : IDisposable {
		public abstract void Select<T>() where T : class, new();

		public abstract void SelectByID<T>(object ID) where T : class, new();

		public abstract Task SelectByIDAsync<T>(object ID) where T : class, new();

		public abstract void Insert<T>(T objectToInsert) where T : class, new();

		public abstract Task InsertAsync<T>(T objectToInsert) where T : class, new();

		public abstract void Update<T>(T objectToUpdate) where T : class, new();

		public abstract Task UpdateAsync<T>(T objectToUpdate) where T : class, new();

		public abstract void Delete<T>(T objectToUpdate) where T : class, new();

		public abstract Task DeleteAsync<T>(T objectToUpdate) where T : class, new();
	}
}
