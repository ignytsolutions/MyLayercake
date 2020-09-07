namespace MyLayercake.Sql {
	public partial class DbDataAdapter {
		/// <summary>
		/// A string representing a raw SQL query. 
		/// This type enables overload resolution between the regular and interpolated <see cref="Select(FormattableString)"/> overloads.
		/// </summary>
		public struct RawSqlString {
			internal string Format;
			public RawSqlString(string s) {
				Format = s;
			}
			public static implicit operator RawSqlString(string value) {
				return new RawSqlString(value);
			}
		}
	}
}
