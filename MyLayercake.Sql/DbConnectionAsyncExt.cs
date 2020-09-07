using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace MyLayercake.Sql {
	internal static class DbConnectionAsyncExt {
		internal static Task OpenAsync(this IDbConnection conn, CancellationToken cancel) {
			if (conn is DbConnection connection) {
				return connection.OpenAsync(cancel);
			} else {
				conn.Open();

				return Task.FromResult(true);
			}
		}
	}
}
