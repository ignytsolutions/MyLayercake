using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyLayercake.Sql.Result {
	/// <summary>
	/// Represents query result that can be mapped to dictionary.
	/// </summary>
	public interface IQueryDictionaryResult {
		/// <summary>
		/// Returns dictionary with first record values.
		/// </summary>
		/// <returns>dictionary with field values or null if query returns zero records.</returns>
		Dictionary<string, object> ToDictionary();

		/// <summary>
		/// Asynchronously returns dictionary with first record values.
		/// </summary>
		Task<Dictionary<string, object>> ToDictionaryAsync(CancellationToken cancel = default(CancellationToken));

		/// <summary>
		/// Returns a list of dictionaries with all query results.
		/// </summary>
		List<Dictionary<string, object>> ToDictionaryList();

		/// <summary>
		/// Asynchronously a list of dictionaries with all query results.
		/// </summary>
		Task<List<Dictionary<string, object>>> ToDictionaryListAsync(CancellationToken cancel = default(CancellationToken));
	}
}
