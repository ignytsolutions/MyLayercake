using MyLayercake.Sql.Query;
using System;

namespace MyLayercake.Sql {
	/// <summary>
	/// Represents SQL builder interface.
	/// </summary>
	public interface ISqlExpressionBuilder
	{
		/// <summary>
		/// Builds SQL-compatible string by specified <see cref="QTable"/>. 
		/// </summary>
		string BuildTableName(QTable tbl);

		/// <summary>
		/// Builds SQL-compatible string by specified <see cref="IQueryValue"/>. 
		/// </summary>
		string BuildValue(IQueryValue v);

		/// <summary>
		/// Builds SQL-compatible string by specified <see cref="QNode"/>.
		/// </summary>
		string BuildExpression(QNode node);
	}
}
