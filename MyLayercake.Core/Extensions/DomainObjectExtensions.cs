using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;

namespace MyLayercake.Core.Extensions {
    public static class DomainObjectExtensions {
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedNonEditableProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedForeignKeyProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static string ClassName<T>(this T domainObject) {
            var type = domainObject.GetType().GetTypeInfo();

            if (type.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayAttribute) is DisplayAttribute displayAttribute)
                return displayAttribute.Description;

            return type.Name;
        }

        public static string PrimaryKeyName<T>(this T domainObject) {
            foreach (PropertyInfo info in domainObject.GetType().GetProperties()) {
                if ((info.GetCustomAttribute(typeof(KeyAttribute), false) as KeyAttribute) != null) {
                    return info.Name;
                }
            }

            return string.Empty;
        }

        public static Type PrimaryKeyType<T>(this T domainObject) {
            foreach (PropertyInfo info in domainObject.GetType().GetProperties()) {
                if ((info.GetCustomAttribute(typeof(KeyAttribute), false) as KeyAttribute) != null) {
                    return info.PropertyType;
                }
            }

            return null;
        }

        public static object PrimaryKeyValue<T>(this T domainObject) {
            foreach (PropertyInfo info in domainObject.GetType().GetProperties()) {
                if ((info.GetCustomAttribute(typeof(KeyAttribute), false) as KeyAttribute) != null) {
                    return info.GetValue(domainObject);
                }
            }

            return null;
        }

        public static PropertyInfo[] Properties<T>(this T domainObject) {
            return domainObject.GetType().GetProperties();
        }
    }
}