using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace MyLayercake.Sql {
	internal static class DbCommandAsyncExt {
		internal static Task<int> ExecuteNonQueryAsync(this IDbCommand cmd, CancellationToken cancel) {
			if (cmd is DbCommand command) {
				return command.ExecuteNonQueryAsync(cancel);
			} else {
				return Task.FromResult<int>( cmd.ExecuteNonQuery() );
			}
		}

		internal static Task<object> ExecuteScalarAsync(this IDbCommand cmd, CancellationToken cancel) {
			if (cmd is DbCommand command) {
				return command.ExecuteScalarAsync(cancel);
			} else {
				return Task.FromResult<object>( cmd.ExecuteScalar() );
			}
		}

	}
}
