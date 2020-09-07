using System.Collections.Generic;

namespace MyLayercake.Sql.Query {
	public class QRawSqlNode : QNode {

		/// <summary>
		/// Nodes collection
		/// </summary>
		public override IList<QNode> Nodes { get { return new QNode[0]; } }

		public string SqlText {
			get; private set;
		}

		public QRawSqlNode(string sqlText) {
			SqlText = sqlText;
		}
	}
}
