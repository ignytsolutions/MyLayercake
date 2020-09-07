using System.Threading;
using System.Threading.Tasks;

namespace MyLayercake.Sql.Result {
	/// <summary>
	/// Represents query result that can be mapped to <see cref="RecordSet"/>.
	/// </summary>
	public interface IQueryRecordSetResult {
		/// <summary>
		/// Returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		RecordSet ToRecordSet();

		/// <summary>
		/// Asynchronously returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		Task<RecordSet> ToRecordSetAsync(CancellationToken cancel = default(CancellationToken));
	}
}
