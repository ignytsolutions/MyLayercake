using System.Collections.Generic;

namespace MyLayercake.Sql.Query {
	/// <summary>
	/// Represents abstract query node that contains child nodes.
	/// </summary>
	//[Serializable]
	public abstract class QNode {
		public abstract IList<QNode> Nodes { get; }

		public string Name { get; set; }

		internal QNode() {
		}

		/// <summary>
		/// OR operator
		/// </summary>
		public static QGroupNode operator |(QNode node1, QNode node2) {
			QGroupNode res = new QGroupNode(QGroupType.Or);
			res.Nodes.Add(node1);
			res.Nodes.Add(node2);
			return res;
		}

		/// <summary>
		/// AND operator
		/// </summary>
		public static QGroupNode operator &(QNode node1, QNode node2) {
			QGroupNode res = new QGroupNode(QGroupType.And);
			res.Nodes.Add(node1);
			res.Nodes.Add(node2);
			return res;
		}
	}
}
