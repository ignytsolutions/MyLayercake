using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyLayercake.Sql {

	/// <summary>
	/// Represents data adapter between database and <see cref="RecordSet"/> models.
	/// </summary>
	public interface IRecordSetAdapter {
		
		/// <summary>
		/// Returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		RecordSet Select(Query.Query q);

		/// <summary>
		/// Asynchronously returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		Task<RecordSet> SelectAsync(Query.Query q);

		/// <summary>
		/// Commits <see cref="RecordSet"/> changes to the database.
		/// </summary>
		int Update(string table, RecordSet rs);

		/// <summary>
		/// An asynchronous version of <see cref="Update(string, RecordSet)"/>
		/// </summary>
		Task<int> UpdateAsync(string table, RecordSet rs);
	}

}
