using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLayercake.BusinessObjects.Datalayer.Enums {
    internal static class DataTypes {
        public static T Get<T>(object value) {
            Type t = typeof(T);
            t = Nullable.GetUnderlyingType(t) ?? t;

            return (value == null || DBNull.Value.Equals(value) || value.ToString() == string.Empty) ?
                default(T) : (T)Convert.ChangeType(value, t);
        }
    }
}
