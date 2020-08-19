using System;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : Attribute {
        public string Value { get; set; }

        public TableName(string TableName) {
            this.Value = TableName;
        }
    }
}