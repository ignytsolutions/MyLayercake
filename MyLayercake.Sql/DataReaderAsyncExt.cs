using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace MyLayercake.Sql {
	internal static class DataReaderAsyncExt {
		internal static Task<bool> ReadAsync(this IDataReader rdr, CancellationToken cancel) {
			if (rdr is DbDataReader reader) {
				return reader.ReadAsync(cancel);
			} else {
				return Task.FromResult(rdr.Read());
			}
		}

		internal static Task<int> GetValuesAsync(this IDataReader rdr, object[] values, CancellationToken cancel) {
			if (rdr is DbDataReader reader) {
				return reader.GetValuesAsync(values, cancel);
			} else {
				return Task.FromResult(rdr.GetValues(values));
			}
		}
	}
}
