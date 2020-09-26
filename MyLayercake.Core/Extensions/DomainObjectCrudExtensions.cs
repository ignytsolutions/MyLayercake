using System.Reflection;
using System.Linq;
using System.Text;

namespace MyLayercake.Core.Extensions {
    public static class DomainObjectCrudExtensions {
        private static readonly string _parameterPrefix = "@";

        public static string BuildDeleteText<T>(this T domainObject) {
            StringBuilder resultText = new StringBuilder();

            resultText.Append(string.Concat("DELETE FROM ", domainObject.ClassName()));
            resultText.Append(string.Concat(" WHERE ", domainObject.PrimaryKeyName(), " = ", _parameterPrefix, domainObject.PrimaryKeyName()));


            return resultText.ToString();
        }

        public static string BuildInsertText<T>(this T domainObject) {
            StringBuilder resultText = new StringBuilder();
            StringBuilder columnsText = new StringBuilder();
            StringBuilder valuesText = new StringBuilder();

            resultText.Append(string.Concat("INSERT INTO ", domainObject.ClassName()));

            foreach (PropertyInfo property in domainObject.Properties()) {
                if (domainObject.Properties().LastOrDefault().Equals(property)) {
                    columnsText.Append(string.Concat(property.Name, ") "));
                    valuesText.Append(string.Concat(_parameterPrefix, property.Name, ")"));
                } else if (domainObject.Properties().FirstOrDefault().Equals(property)) {
                    columnsText.Append(string.Concat(" (", property.Name, ", "));
                    valuesText.Append(string.Concat(" VALUES (", _parameterPrefix, property.Name, ", "));
                } else {
                    columnsText.Append(string.Concat(property.Name, ", "));
                    valuesText.Append(string.Concat(_parameterPrefix, property.Name, ", "));
                }
            }

            resultText.Append(columnsText);
            resultText.Append(valuesText);

            return resultText.ToString();
        }

        public static string BuildUpdateText<T>(this T domainObject) {
            StringBuilder resultText = new StringBuilder();

            resultText.Append(string.Concat("UPDATE ", domainObject.ClassName()));

            foreach (PropertyInfo property in domainObject.Properties()) {
                if (domainObject.Properties().LastOrDefault().Equals(property)) {
                    resultText.Append(string.Concat(property.Name, " = ", _parameterPrefix, property.Name));
                } else if (domainObject.Properties().FirstOrDefault().Equals(property)) {
                    resultText.Append(string.Concat(" SET ", property.Name, " = ", _parameterPrefix, property.Name, ","));
                } else {
                    resultText.Append(string.Concat(property.Name, " = ", _parameterPrefix, property.Name, ","));
                }
            }

            resultText.Append(string.Concat(" WHERE ", domainObject.PrimaryKeyName(), " = ", _parameterPrefix, domainObject.PrimaryKeyName()));

            return resultText.ToString();
        }

        public static string BuildSelectText<T>(this T domainObject) {
            StringBuilder resultText = new StringBuilder();

            resultText.Append(string.Concat("SELECT "));

            foreach (PropertyInfo property in domainObject.Properties()) {
                if (domainObject.Properties().LastOrDefault().Equals(property)) {
                    resultText.Append(string.Concat(property.Name));
                } else {
                    resultText.Append(string.Concat(property.Name, ", "));
                }
            }

            resultText.Append(string.Concat(" FROM ", domainObject.ClassName()));

            return resultText.ToString();
        }

        public static string BuildSelectByIDText<T>(this T domainObject) {
            StringBuilder resultText = new StringBuilder();

            resultText.Append(string.Concat("SELECT "));

            foreach (PropertyInfo property in domainObject.Properties()) {
                if (domainObject.Properties().LastOrDefault().Equals(property)) {
                    resultText.Append(string.Concat(property.Name));
                } else {
                    resultText.Append(string.Concat(property.Name, ", "));
                }
            }

            resultText.Append(string.Concat("FROM ", domainObject.ClassName()));
            resultText.Append(string.Concat("WHERE ", domainObject.PrimaryKeyName(), " = ", _parameterPrefix, domainObject.PrimaryKeyName()));

            return resultText.ToString();
        }
    }
}