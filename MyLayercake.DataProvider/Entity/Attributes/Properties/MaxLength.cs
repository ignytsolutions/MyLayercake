using System;

namespace MyLayercake.DataProvider.Entity.Attributes.Properties {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxLength : Attribute {
        public int Value { get; set; }

        public MaxLength(int Value) {
            this.Value = Value;
        }
    }
}
