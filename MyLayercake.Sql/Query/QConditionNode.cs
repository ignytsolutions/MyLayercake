using System;
using System.Collections.Generic;

namespace MyLayercake.Sql.Query {
	public class QConditionNode : QNode {
		private IQueryValue _LValue;
		private Conditions _Condition;
		private IQueryValue _RValue;

		public IQueryValue LValue {
			get { return _LValue; }
		}

		public Conditions Condition {
			get { return _Condition; }
		}

		public IQueryValue RValue {
			get { return _RValue; }
		}

		public override IList<QNode> Nodes {
			get {
				var l = new List<QNode>();
				if (LValue is QNode)
					l.Add((QNode)LValue);
				if (RValue is QNode)
					l.Add((QNode)RValue);
				return l;
			}
		}

		public QConditionNode(IQueryValue lvalue, Conditions conditions, IQueryValue rvalue) {
			_RValue = rvalue;
			_Condition = conditions;
			_LValue = lvalue;
		}

		public QConditionNode(string name, IQueryValue lvalue, Conditions conditions, IQueryValue rvalue) :
			this(lvalue, conditions, rvalue) {
			Name = name;
		}

		public QConditionNode(QConditionNode node) {
			Name = node.Name;
			_LValue = node.LValue;
			_Condition = node.Condition;
			_RValue = node.RValue;
		}
	}

	[Flags]
	public enum Conditions {
		Equal = 1,
		LessThan = 2,
		GreaterThan = 4,
		Like = 8,
		In = 16,
		Null = 32,
		Not = 64
	}
}
