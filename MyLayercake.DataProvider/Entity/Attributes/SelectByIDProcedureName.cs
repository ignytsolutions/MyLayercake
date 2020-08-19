using System;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class SelectByIDProcedureName : Attribute {
        public string Value { get; set; }

        public SelectByIDProcedureName(string ProcedureName) {
            this.Value = ProcedureName;
        }
    }
}
