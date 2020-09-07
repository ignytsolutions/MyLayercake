using System.Threading;
using System.Threading.Tasks;

using System.Data;

namespace MyLayercake.Sql.Result {
	/// <summary>
	/// Represents query result that can be mapped to <see cref="DataTable"/>.
	/// </summary>
	/// <remarks>This interface is not available in netstandard1.5 build.</remarks>
	public interface IQueryDataTableResult {
		/// <summary>
		/// Returns all query results as <see cref="DataTable"/>.
		/// </summary>
		DataTable ToDataTable();

		/// <summary>
		/// Asynchronously returns all query results as <see cref="DataTable"/>.
		/// </summary>
		Task<DataTable> ToDataTableAsync(CancellationToken cancel = default(CancellationToken));

		/// <summary>
		/// Loads all query results into specified <see cref="DataTable"/>.
		/// </summary>
		DataTable ToDataTable(DataTable tbl);

		/// <summary>
		/// Asynchronously loads all query results into specified <see cref="DataTable"/>.
		/// </summary>
		Task<DataTable> ToDataTableAsync(DataTable tbl, CancellationToken cancel = default(CancellationToken));
	}
}
