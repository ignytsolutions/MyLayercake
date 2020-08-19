using System;

namespace MyLayercake.DataProvider.Entity.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class DeleteProcedureName : Attribute {
        public string Value { get; set; }

        public DeleteProcedureName(string ProcedureName) {
            Value = ProcedureName;
        }
    }
}
