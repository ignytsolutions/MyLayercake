using System;

namespace MyLayercake.DataProvider.Entity.Attributes.Properties {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IsRequired : Attribute {
        public bool Value { get; set; }

        public IsRequired() {
            this.Value = true;
        }
    }
}
