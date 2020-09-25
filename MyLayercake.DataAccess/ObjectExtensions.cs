using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;

namespace MyLayercake.DataAccess {
    public abstract class ObjectExtensions {
		/// <summary>
		/// Method for adding in a Dictionary of parameters
		/// </summary>
		/// <param name="command">The command to add the parameters to.</param>
		/// <param name="parameters">The parameters to convert to parameters.</param>
		public void AddParams(DbCommand command, Dictionary<string, object> parameters) {
			if (parameters == null) return;

			foreach (var item in parameters) {
				AddParam(command, item);
			}
		}

		/// <summary>
		/// Method for adding a parameter
		/// </summary>
		/// <param name="command">The command to add the parameters to.</param>
		/// <param name="parameter">The parameter values to convert to parameters.</param>
		public abstract void AddParam(DbCommand command, KeyValuePair<string, object> parameter);


		/// <summary>
		/// Turns an IDataReader to a Dynamic list of things
		/// </summary>
		/// <param name="reader">The datareader which rows to convert to a list of expandos.</param>
		/// <returns>List of expandos, one for every row read.</returns>
		public static List<dynamic> ToExpandoList(IDataReader reader) {
			var result = new List<dynamic>();

			while (reader.Read())
				result.Add(RecordToExpando(reader));

			return result;
		}


		/// <summary>
		/// Converts the current row the datareader points to to a new Expando object.
		/// </summary>
		/// <param name="reader">The RDR.</param>
		/// <returns>expando object which contains the values of the row the reader points to</returns>
		public static dynamic RecordToExpando(IDataReader reader) {
			dynamic epandoObject = new ExpandoObject();

			var dictionary = (IDictionary<string, object>)epandoObject;

			object[] values = new object[reader.FieldCount];
			reader.GetValues(values);

			for (int i = 0; i < values.Length; i++) {
				var v = values[i];

				dictionary.Add(reader.GetName(i), DBNull.Value.Equals(v) ? null : v);
			}

			return epandoObject;
		}


		/// <summary>
		/// Turns the object into an ExpandoObject 
		/// </summary>
		/// <param name="thingy">The object to convert.</param>
		/// <returns>A new expando object with the values of the passed in object</returns>
		public static dynamic ToExpando(object thingy) {
			if (thingy is ExpandoObject) return thingy;

			var result = new ExpandoObject();
			var d = (IDictionary<string, object>)result; //work with the Expando as a Dictionary

			if (thingy.GetType() == typeof(NameValueCollection) || thingy.GetType().IsSubclassOf(typeof(NameValueCollection))) {
				var nv = (NameValueCollection)thingy;

				nv.Cast<string>().Select(key => new KeyValuePair<string, object>(key, nv[key])).ToList().ForEach(i => d.Add(i));
			} else {
				var props = thingy.GetType().GetProperties();

				foreach (var item in props) {
					d.Add(item.Name, item.GetValue(thingy, null));
				}
			}

			return result;
		}


		/// <summary>
		/// Turns the object into a Dictionary with for each property a name-value pair, with name as key.
		/// </summary>
		/// <param name="thingy">The object to convert to a dictionary.</param>
		/// <returns></returns>
		public static IDictionary<string, object> ToDictionary(object thingy) {
			return (IDictionary<string, object>)ToExpando(thingy);
		}


		/// <summary>
		/// Extension method to convert dynamic data to a DataTable. Useful for databinding.
		/// </summary>
		/// <param name="items"></param>
		/// <returns>A DataTable with the copied dynamic data.</returns>
		public static DataTable ToDataTable(IEnumerable<dynamic> items) {
			var data = items.ToArray();
			var toReturn = new DataTable();

			if (!data.Any()) {
				return toReturn;
			}

			foreach (var kvp in (IDictionary<string, object>)data[0]) {
				// for now we'll fall back to string if the value is null, as we don't know any type information on null values.
				var type = kvp.Value == null ? typeof(string) : kvp.Value.GetType();
				toReturn.Columns.Add(kvp.Key, type);
			}

			return ToDataTable(data, toReturn);
		}


		/// <summary>
		/// Extension method to convert dynamic data to a DataTable. Useful for databinding.
		/// </summary>
		/// <param name="items">The items to convert to data rows.</param>
		/// <param name="toFill">The datatable to fill. It's required this datatable has the proper columns setup.</param>
		/// <returns>
		/// toFill with the data from items.
		/// </returns>
		public static DataTable ToDataTable(IEnumerable<dynamic> items, DataTable toFill) {
			dynamic[] data = items is dynamic[] v ? v : items.ToArray();

			if (toFill == null || toFill.Columns.Count <= 0)
				return toFill;

			foreach (var d in data)
				toFill.Rows.Add(((IDictionary<string, object>)d).Values.ToArray());

			return toFill;
		}
	}
}
