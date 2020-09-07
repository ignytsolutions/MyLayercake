using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace MyLayercake.Sql {

	/// <summary>
	/// Extension methods for <see cref="IDbCommandBuilder"/> interface.
	/// </summary>
	public static class DbCommandBuilderExtensions {

		public static IDbCommand GetUpdateCommand(this IDbCommandBuilder cmdBuilder, Query.Query q, IDictionary<string,object> data) {
			return cmdBuilder.GetUpdateCommand(q, DataHelper.GetChangeset(data) );
		}
		public static IDbCommand GetUpdateCommand(this IDbCommandBuilder cmdBuilder, Query.Query q, object poco) {
			return cmdBuilder.GetUpdateCommand(q, DataHelper.GetChangeset(poco, null) );
		}

		public static IDbCommand GetInsertCommand(this IDbCommandBuilder cmdBuilder, string table, IDictionary<string,object> data) {
			return cmdBuilder.GetInsertCommand(table, DataHelper.GetChangeset(data) );
		}
		public static IDbCommand GetInsertCommand(this IDbCommandBuilder cmdBuilder, string table, object poco) {
			return cmdBuilder.GetInsertCommand(table, DataHelper.GetChangeset(poco, null) );
		}

	}
}
