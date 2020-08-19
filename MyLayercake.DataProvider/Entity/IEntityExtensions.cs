using MyLayercake.DataProvider.Entity.Attributes.Properties;
using System;
using System.Reflection;

namespace MyLayercake.DataProvider.Entity {
    internal static class IEntityExtensions {
        public static string DeleteProcedureName<T>(this T input) where T : IEntity {
            if ((input.GetType().GetCustomAttribute(typeof(Attributes.DeleteProcedureName), false) as Attributes.DeleteProcedureName) != null) {
                return (input.GetType().GetCustomAttribute(typeof(Attributes.DeleteProcedureName), false) as Attributes.DeleteProcedureName).Value;
            } else {
                return null;
            }
        }

        public static string InsertProcedureName<T>(this T input) where T : IEntity {
            if ((input.GetType().GetCustomAttribute(typeof(Attributes.InsertProcedureName), false) as Attributes.InsertProcedureName) != null) {
                return (input.GetType().GetCustomAttribute(typeof(Attributes.InsertProcedureName), false) as Attributes.InsertProcedureName).Value;
            } else {
                return null;
            }
        }

        public static string SelectProcedureName<T>(this T input) where T : IEntity {
            if ((input.GetType().GetCustomAttribute(typeof(Attributes.SelectProcedureName), false) as Attributes.SelectProcedureName) != null) {
                return (input.GetType().GetCustomAttribute(typeof(Attributes.SelectProcedureName), false) as Attributes.SelectProcedureName).Value;
            } else {
                return null;
            }
        }

        public static string SelectByIDProcedureName<T>(this T input) where T : IEntity {
            if ((input.GetType().GetCustomAttribute(typeof(Attributes.SelectByIDProcedureName), false) as Attributes.SelectByIDProcedureName) != null) {
                return (input.GetType().GetCustomAttribute(typeof(Attributes.SelectByIDProcedureName), false) as Attributes.SelectByIDProcedureName).Value;
            } else {
                return null;
            }
        }

        public static object PrimaryKeyValue<T>(this T input) where T : IEntity {
            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                if ((info.GetCustomAttribute(typeof(IsPrimaryKey), false) as IsPrimaryKey).Value) {
                    return info.GetValue(input);
                }
            }

            return null;
        }

        public static string PrimaryKeyName<T>(this T input) where T : IEntity {
            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                if ((info.GetCustomAttribute(typeof(IsPrimaryKey), false) as IsPrimaryKey).Value) {
                    return info.Name;
                }
            }

            return string.Empty;
        }

        public static string FileName<T>(this T input,string extension) where T : IEntity {
            string _fileName = string.Empty;

            _fileName = typeof(T).Name;

            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                if ((info.GetCustomAttribute(typeof(InFileName), false) != null)) {
                    if ((info.GetCustomAttribute(typeof(InFileName), false) as InFileName).Value) {
                        _fileName = String.Format("{0}_{1}", _fileName, info.GetValue(input));
                    }
                }
            }

            _fileName = String.Format("{0}_{1}", _fileName, DateTime.Now.ToString("yyyyMMddHHmmss"));

            _fileName = String.Format("{0}.{1}", _fileName, extension);

            return _fileName;
        }

        public static string TableName<T>(this T input) where T : IEntity {
            return (input.GetType().GetCustomAttribute(typeof(Attributes.TableName), false) as Attributes.TableName).Value;
        }

        public static string UpdateProcedureName<T>(this T input) where T : IEntity {
            if ((input.GetType().GetCustomAttribute(typeof(Attributes.UpdateProcedureName), false) as Attributes.UpdateProcedureName) != null) {
                return (input.GetType().GetCustomAttribute(typeof(Attributes.UpdateProcedureName), false) as Attributes.UpdateProcedureName).Value;
            } else {
                return null;
            }
        }
    }
}
