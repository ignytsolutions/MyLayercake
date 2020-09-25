using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using MyLayercake.Core.Attributes;

namespace MyLayercake.Core.Extentions {
    public static class DomainObjectExtensions {
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedNonEditableProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();
        private static ConcurrentDictionary<Type, PropertyInfo[]> _cachedForeignKeyProperties = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static string ClassName<T>(this T domainObject) {
            var type = domainObject.GetType().GetTypeInfo();

            if (type.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute) is DisplayNameAttribute displayAttribute)
                return displayAttribute.DisplayName;

            return type.Name;
        }

        public static string PrimaryKeyName<T>(this T domainObject) {
            foreach (PropertyInfo info in domainObject.GetType().GetProperties()) {
                if ((info.GetCustomAttribute(typeof(Attributes.KeyAttribute), false) as Attributes.KeyAttribute) != null) {
                    return info.Name;
                }
            }

            return string.Empty;
        }

        public static object PrimaryKeyValue<T>(this T domainObject) {
            foreach (PropertyInfo info in domainObject.GetType().GetProperties()) {
                if ((info.GetCustomAttribute(typeof(Attributes.KeyAttribute), false) as Attributes.KeyAttribute) != null) {
                    return info.GetValue(domainObject);
                }
            }

            return null;
        }

        public static PropertyInfo[] Properties<T>(this T domainObject) {
            return domainObject.GetType().GetProperties();
        }

        /// <summary>
        /// Changes property values from 0 to NULL for any Nullable<int> property marked with the PeasyForeignKeyAttribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="domainObject"></param>
        public static void RevertForeignKeysFromZeroToNull<T>(this T domainObject) {
            var foreignKeyProperties = domainObject.GetCachedForeignKeyProperties();

            foreach (var property in foreignKeyProperties) {
                var id = 0;
                if (property.GetValue(domainObject) != null) {
                    if (int.TryParse(property.GetValue(domainObject).ToString(), out id)) {
                        if (id == 0)
                            property.SetValue(domainObject, null);
                    }
                }
            }
        }

        public static void RevertNonEditableValues<T>(this T changedObject, T originalObject) {
            var nonEditableProperties = changedObject.GetCachedNonEditableProperties();

            foreach (var property in nonEditableProperties) {
                var originalValue = property.GetValue(originalObject);
                property.SetValue(changedObject, originalValue);
            }
        }

        private static IEnumerable<PropertyInfo> GetCachedForeignKeyProperties<T>(this T domainObject) {
            var type = typeof(T);
            if (!_cachedForeignKeyProperties.ContainsKey(type)) {
                var foreignKeyProps = type.GetRuntimeProperties()
                                          .Where(p => p.GetCustomAttributes(typeof(ForeignKeyAttribute), true)
                                                       .Any())
                                          // Enforce that only properties of type Nullable<int> marked with the PeasyForeignKey attribute are selected
                                          //.Where(p => p.PropertyType.gener p.PropertyType.IsGenericType)
                                          .Where(p => p.PropertyType == typeof(Nullable<int>));

                _cachedForeignKeyProperties[type] = foreignKeyProps.ToArray();
            }
            return _cachedForeignKeyProperties[type];
        }

        private static IEnumerable<PropertyInfo> GetCachedNonEditableProperties<T>(this T domainObject) {
            var type = typeof(T);

            if (!_cachedNonEditableProperties.ContainsKey(type)) {
                var nonEditableProperties = type.GetRuntimeProperties()
                                                .Where(p => p.GetCustomAttributes(typeof(EditableAttribute), true)
                                                             .Cast<EditableAttribute>()
                                                             .Any(a => a.AllowEdit == false));

                _cachedNonEditableProperties[type] = nonEditableProperties.ToArray();
            }
            return _cachedNonEditableProperties[type];
        }
    }
}