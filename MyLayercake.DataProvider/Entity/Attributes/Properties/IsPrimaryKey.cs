using System;

namespace MyLayercake.DataProvider.Entity.Attributes.Properties {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IsPrimaryKey : Attribute {
        public bool Value { get; set; }

        public IsPrimaryKey() {
            this.Value = true;
        }
    }
}
