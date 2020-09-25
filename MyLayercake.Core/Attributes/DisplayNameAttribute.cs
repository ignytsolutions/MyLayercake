using System;
using System.Collections.Generic;
using System.Text;

namespace MyLayercake.Core.Attributes {
    public class DisplayNameAttribute : Attribute {
        public DisplayNameAttribute(string displayName) {
            DisplayName = displayName;
        }

        public string DisplayName { get; private set; }
    }
}
