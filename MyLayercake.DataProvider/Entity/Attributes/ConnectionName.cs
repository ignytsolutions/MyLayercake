using System;
using System.Linq;

namespace MyLayercake.DataProvider.Entity.Attributes {
    [System.AttributeUsage(AttributeTargets.Class)]
    public class ConnectionName : System.Attribute {
        public string Value { get; set; }

        public ConnectionName(string value) {
            this.Value = value;
        }

        public static ConnectionName GetConnection(Type type) {
            return (ConnectionName)type.GetCustomAttributes(false).SingleOrDefault(x => x is ConnectionName);
        }
    }
}
