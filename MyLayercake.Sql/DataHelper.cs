using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MyLayercake.Sql.Query;

namespace MyLayercake.Sql {
	internal static class DataHelper {

		internal static bool IsNullOrDBNull(object v) {
			return v == null || DBNull.Value.Equals(v);
		}

		internal static void EnsureConnectionOpen(IDbConnection connection, Action a) {
			bool closeConn = false;
			if (connection.State != ConnectionState.Open) {
				connection.Open();
				closeConn = true;
			}
			try {
				a();
			} finally {
				if (closeConn && connection.State != ConnectionState.Closed)
					connection.Close();
			}
		}

		internal static QNode MapQValue(QNode qNode, Func<IQueryValue, IQueryValue> mapFunc) {
			if (qNode is QGroupNode) {
				var group = new QGroupNode((QGroupNode)qNode);
				for (int i = 0; i < group.Nodes.Count; i++)
					group.Nodes[i] = MapQValue(group.Nodes[i], mapFunc);
				return group;
			}

			if (qNode is QConditionNode) {
				var origCndNode = (QConditionNode)qNode;
				var cndNode = new QConditionNode(origCndNode.Name,
						mapFunc(origCndNode.LValue),
						origCndNode.Condition,
						mapFunc(origCndNode.RValue));
				return cndNode;
			}

			if (qNode is QNegationNode) {
				var negNode = new QNegationNode((QNegationNode)qNode);
				for (int i = 0; i < negNode.Nodes.Count; i++)
					negNode.Nodes[i] = MapQValue(negNode.Nodes[i], mapFunc);
				return negNode;
			}

			return qNode;
		}

		internal static RecordSet GetRecordSetByReader(IDataReader rdr) {
			var rsCols = new List<RecordSet.Column>(rdr.FieldCount);
			var rsPkCols = new List<RecordSet.Column>();

			if (rsCols.Count == 0) {
				// lets suggest columns by standard IDataReader interface
				for (int i = 0; i < rdr.FieldCount; i++) {
					var colName = rdr.GetName(i);
					var colType = rdr.GetFieldType(i);
					rsCols.Add(new RecordSet.Column(colName, colType));
				}
			}
			var rs = new RecordSet(rsCols.ToArray(), 1);
			if (rsPkCols.Count > 0)
				rs.PrimaryKey = rsPkCols.ToArray();
			return rs;
		}

		internal static void EnsureDataTableColumnsByReader(DataTable tbl, IDataReader rdr) {
			// lets suggest columns by standard IDataReader interface
			for (int i = 0; i < rdr.FieldCount; i++) {
				var colName = rdr.GetName(i);
				var colType = rdr.GetFieldType(i);
				if (!tbl.Columns.Contains(colName)) {
					tbl.Columns.Add(colName, colType);
				}
			}
		}

		internal static IEnumerable<KeyValuePair<string, IQueryValue>> GetChangeset(IDictionary data) {
			if (data == null)
				yield break;
			foreach (DictionaryEntry entry in data) {
				var qVal = entry.Value is IQueryValue ? (IQueryValue)entry.Value : new QConst(entry.Value);
				yield return new KeyValuePair<string, IQueryValue>(Convert.ToString(entry.Key), qVal);
			}
		}

		internal static IEnumerable<KeyValuePair<string, IQueryValue>> GetChangeset(IDictionary<string, object> data) {
			if (data == null)
				yield break;
			foreach (var entry in data) {
				var qVal = entry.Value is IQueryValue ? (IQueryValue)entry.Value : new QConst(entry.Value);
				yield return new KeyValuePair<string, IQueryValue>(entry.Key, qVal);
			}
		}

		internal static IEnumerable<KeyValuePair<string, IQueryValue>> GetChangeset(object o, DataMapper dtoMapper) {
			if (o == null)
				yield break;
			var oType = o.GetType();
			var schema = (dtoMapper ?? DataMapper.Instance).GetSchema(oType);
			foreach (var columnMapping in schema.Columns) {
				if (columnMapping.IsReadOnly || columnMapping.GetVal == null)
					continue;
				var pVal = columnMapping.GetVal(o);
				var qVal = pVal is IQueryValue ? (IQueryValue)pVal : new QConst(pVal);
				var fldName = columnMapping.ColumnName;
				yield return new KeyValuePair<string, IQueryValue>(fldName, qVal);
			}
		}

		internal static bool IsSimpleIdentifier(string s) {
			if (s != null)
				for (int i = 0; i < s.Length; i++) {
					var ch = s[i];
					if (!Char.IsLetterOrDigit(ch) && ch != '-' && ch != '_')
						return false;
				}
			return true;
		}
	}
}
