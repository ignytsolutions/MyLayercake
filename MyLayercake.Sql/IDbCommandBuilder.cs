using System.Data;
using System.Collections.Generic;
using MyLayercake.Sql.Query;

namespace MyLayercake.Sql {

	/// <summary>
	/// Automatically generates single-table commands to create-update-delete-retrieve database records.
	/// </summary>
	public interface IDbCommandBuilder 
	{
		IDbCommand GetSelectCommand(Query.Query query);

		IDbCommand GetInsertCommand(string tableName, IEnumerable<KeyValuePair<string,IQueryValue>> data);

		IDbCommand GetDeleteCommand(Query.Query query);

		IDbCommand GetUpdateCommand(Query.Query query, IEnumerable<KeyValuePair<string,IQueryValue>> data);

		IDbFactory DbFactory { get; }
	}
}
