using System;
using System.Text;
using System.Linq;

namespace MyLayercake.Sql.Query {
	/// <summary>
	/// Represents query aggregate field.
	/// </summary>
	public class QAggregateField : QField
	{
		/// <summary>
		/// Aggregate function name.
		/// </summary>
		public string AggregateFunction { get; private set; }

		/// <summary>
		/// Aggregate function arguments.
		/// </summary>
		public QField[] Arguments { get; private set; }

		/// <summary>
		/// Initializes a new instance of QAggregateField.
		/// </summary>
		/// <param name="fld">field name</param>
		/// <param name="aggregateFunction">aggregate function name</param>
		/// <param name="argFields">list of arguments for the aggregate function</param>
		public QAggregateField(string fld, string aggregateFunction, params QField[] argFields) 
			: base(null, fld, GetAggrExpr(aggregateFunction, argFields.Select(f=>f.ToString()).ToArray() ) ) {
			AggregateFunction = aggregateFunction;
			Arguments = argFields;
		}

		private static char[] CustomAggrSqlChars = new[] {'(', '{' };

		internal static string GetAggrExpr(string aggrFunc, string[] args) {
			if (aggrFunc.IndexOfAny(CustomAggrSqlChars) >= 0)
				return String.Format(aggrFunc, args);

			var sb = new StringBuilder(aggrFunc);
			sb.Append('(');
			for (int i = 0; i < args.Length; i++) {
				if (i > 0)
					sb.Append(',');
				sb.Append(args[i].ToString());
			}
			sb.Append(')');
			return sb.ToString();
		}

		/// <summary>
		/// Returns a string representation of QField
		/// </summary>
		/// <returns>string in [prefix].[field name] format</returns>
		public override string ToString() {
			return String.IsNullOrEmpty(Prefix) ? Name : Prefix+"."+Name;
		}
	}
}
