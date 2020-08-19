using System;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class SelectProcedureName : Attribute {
        public string Value { get; set; }

        public SelectProcedureName(string ProcedureName) {
            this.Value = ProcedureName;
        }
    }
}
