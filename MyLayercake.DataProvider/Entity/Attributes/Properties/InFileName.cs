using System;

namespace MyLayercake.DataProvider.Entity.Attributes.Properties {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class InFileName : Attribute {
        public bool Value { get; set; }

        public InFileName(bool Value) {
            this.Value = Value;
        }
    }
}
