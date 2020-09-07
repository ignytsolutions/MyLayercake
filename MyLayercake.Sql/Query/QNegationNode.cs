using System.Collections.Generic;

namespace MyLayercake.Sql.Query {
	/// <summary>
	/// Represents logical negation operator
	/// </summary>
	public class QNegationNode : QNode {

		private QNode[] SingleNodeList;

		public override IList<QNode> Nodes {
			get { return SingleNodeList; }
		}

		/// <summary>
		/// Initializes a new instance of the QueryNegationNode that wraps specified node  
		/// </summary>
		/// <param name="node">condition node to negate</param>
		public QNegationNode(QNode node) {
			SingleNodeList = new QNode[] { node };
		}

		public QNegationNode(QNegationNode copyNode) {
			SingleNodeList = new QNode[] { copyNode.Nodes[0] };
			Name = copyNode.Name;
		}
	}
}
