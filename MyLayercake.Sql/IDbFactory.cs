using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyLayercake.Sql {

	/// <summary>
	/// Represents factory for creating db-specific ADO.NET component implementations.
	/// </summary>
	public interface IDbFactory {

		/// <summary>
		/// Create command 
		/// </summary>
		IDbCommand CreateCommand();

		/// <summary>
		/// Create connection 
		/// </summary>
		IDbConnection CreateConnection();

		/// <summary>
		/// Add new constant parameter
		/// </summary>
		CommandParameter AddCommandParameter(IDbCommand cmd, object value);

		/// <summary>
		/// Creare SQL builder
		/// </summary>
		ISqlExpressionBuilder CreateSqlBuilder(IDbCommand dbCommand, Func<Query.Query, string> buildSubquery);

		/// <summary>
		/// Gets ID of last inserted record
		/// </summary>
		object GetInsertId(IDbConnection connection, IDbTransaction transaction);

		/// <summary>
		/// Asynchronously gets ID of last inserted record
		/// </summary>
		Task<object> GetInsertIdAsync(IDbConnection connection, IDbTransaction transaction, CancellationToken cancel);
	}

	public sealed class CommandParameter {
		public string Placeholder { get; private set; }
		public IDbDataParameter Parameter { get; private set; }

		public CommandParameter(string placeholder, IDbDataParameter dbParam) {
			Placeholder = placeholder;
			Parameter = dbParam;
		}
	}
}
