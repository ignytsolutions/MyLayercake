using System;
using System.Data;
using MyLayercake.Sql.Query;

namespace MyLayercake.Sql {
	/// <summary>
	/// Generic implementation of DB-specific SQL expression builder.
	/// </summary>
	public class DbSqlExpressionBuilder : SqlExpressionBuilder {
		protected IDbCommand Command { get; private set; }
		protected IDbFactory DbFactory { get; private set; }

		public Func<Query.Query, string> BuildSubquery { get; set; }

		public Func<string, string> FormatIdentifier { get; set; }

		public DbSqlExpressionBuilder(IDbCommand cmd, IDbFactory dbFactory) {
			Command = cmd;
			DbFactory = dbFactory;
		}

		protected override string BuildConditionLValue(QConditionNode node) {
			var lValue = base.BuildConditionLValue(node);
			return (node.LValue is Query.Query) ? "(" + lValue + ")" : lValue;
		}

		protected override string BuildConditionRValue(QConditionNode node) {
			var rValue = base.BuildConditionRValue(node);
			return (node.RValue is Query.Query && ((node.Condition & Conditions.In) != Conditions.In)) ?
				"(" + rValue + ")" : rValue;
		}

		public override string BuildValue(IQueryValue v) {
			if (v is Query.Query) {
				// refactoring is needed for subqueries handling. TBD: find better solution without 'buildSubquery' delegate.
				if (BuildSubquery == null)
					throw new NotImplementedException("Subqueries are not supported in this context");
				return BuildSubquery((Query.Query)v);
			}
			return base.BuildValue(v);
		}

		protected override string BuildValue(QConst value) {
			object constValue = value.Value;

			// do not use parameter for nulls (type param cannot be determined by null)
			if (constValue == null && !(value is QVar))
				return "NULL";

			// all non-null constants are passed as parameters						
			var cmdParam = DbFactory.AddCommandParameter(Command, constValue);
			if (value is QVar) {
				cmdParam.Parameter.SourceColumn = ((QVar)value).Name;
			}
			return cmdParam.Placeholder;
		}

		protected override string BuildValue(string str) {
			return DbFactory.AddCommandParameter(Command, str).Placeholder;
		}

		protected override string BuildIdentifier(string name) {
			return FormatIdentifier != null ? FormatIdentifier(name) : name;
		}
	}
}
