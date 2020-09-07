namespace MyLayercake.Sql.Query {
	/// <summary>
	/// Represents raw SQL query value
	/// </summary>
	//[Serializable]
	public class QRawSql : IQueryValue {

		/// <summary>
		/// Get SQL text
		/// </summary>
		public string SqlText {
			get; private set;
		}

		/// <summary>
		/// Initializes a new instance of the QRawSql with specfield SQL text
		/// </summary>
		/// <param name="sqlText"></param>
		public QRawSql(string sqlText) {
			SqlText = sqlText;
		}
	}
}
