using System;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class UpdateProcedureName : Attribute {
        public string Value { get; set; }

        public UpdateProcedureName(string ProcedureName) {
            this.Value = ProcedureName;
        }
    }
}
