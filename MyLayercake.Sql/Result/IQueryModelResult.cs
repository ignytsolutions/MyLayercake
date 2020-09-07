using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyLayercake.Sql.Result {
	/// <summary>
	/// Represents query result that can be mapped to POCO model.
	/// </summary>
	public interface IQueryModelResult {
		/// <summary>
		/// Returns the first record from the query result. 
		/// </summary>
		/// <returns>depending on T, single value or all fields values from the first record</returns>
		T Single<T>();

		/// <summary>
		/// Asynchronously returns the first record from the query result. 
		/// </summary>
		/// <returns>depending on T, single value or all fields values from the first record</returns>
		Task<T> SingleAsync<T>(CancellationToken cancel = default(CancellationToken));

		/// <summary>
		/// Returns a list with all query results.
		/// </summary>
		/// <returns>list with query results</returns>
		List<T> ToList<T>();

		/// <summary>
		/// Asynchronously returns a list with all query results.
		/// </summary>
		Task<List<T>> ToListAsync<T>(CancellationToken cancel = default(CancellationToken));
	}
}
