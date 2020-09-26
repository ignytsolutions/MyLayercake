using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Linq;

namespace MyLayercake.Core.Extensions {
    internal static class IDataReaderExtentions {
        public static IEnumerable<T> DomainObjects<T>(this IDataReader reader) where T : IEntity, new() {
            T tempObject;
            Collection<T> objects = new Collection<T>();

            while (reader.Read()) {
                tempObject = new T();

                for (int i = 0; i < reader.FieldCount; i++) {
                    SetPropValue(reader.GetName(i), tempObject, typeof(T), reader.GetValue(i));
                }

                objects.Add(tempObject);
            }

            reader.Close();

            return objects;
        }
        public static T DomainObject<T>(this IDataReader reader) where T : IEntity, new() {
            T tempObject = new T();

            while (reader.Read()) {
                for (int i = 0; i < reader.FieldCount; i++) {
                    SetPropValue(reader.GetName(i), tempObject, typeof(T), reader.GetValue(i));
                }
            }

            reader.Close();

            return tempObject;
        }

        private static void SetPropValue(string name, object obj, Type type, object value) {
            var parts = name.Split('.').ToList();
            var currentPart = parts[0];

            PropertyInfo info = type.GetProperty(currentPart);

            if (info == null) { throw new Exception(String.Format("Cannot map Reader column {0} to object {1}", value, obj.GetType().Name)); ; }

            if (name.IndexOf(".") > -1) {
                parts.Remove(currentPart);
                SetPropValue(string.Join(".", parts), info.GetValue(obj, null), info.PropertyType, value);
            } else {
                if (value == DBNull.Value)
                    value = null;

                info.SetValue(obj, value, null);
            }
        }
    }
}