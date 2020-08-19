using System;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class InsertProcedureName : Attribute {
        public string Value { get; set; }

        public InsertProcedureName(string ProcedureName) {
            this.Value = ProcedureName;
        }
    }
}
