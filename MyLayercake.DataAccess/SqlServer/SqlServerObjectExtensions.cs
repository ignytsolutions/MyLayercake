using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;

namespace MyLayercake.DataAccess.SqlServer {
	/// <summary>
	/// Class which provides methods for various ADO.NET objects.
	/// </summary>
	public class SqlServerObjectExtensions : ObjectExtensions {
		/// <summary>
		/// Extension for adding single parameter. 
		/// </summary>
		/// <param name="command">The command to add the parameter to.</param>
		/// <param name="parameter">The value to add as a parameter to the command.</param>
		public override void AddParam(DbCommand command, KeyValuePair<string,object> parameter) {
			var para = command.CreateParameter();

			para.ParameterName = string.Format("@{0}", parameter.Key);

			if (parameter.Value == null) {
				para.Value = DBNull.Value;
			} else {
				if(parameter.Value as ExpandoObject == null)
					para.Value = parameter.Value;
				else
					para.Value = ((IDictionary<string, object>)parameter.Value).Values.FirstOrDefault();
			}

			command.Parameters.Add(para);
		}
	}
}
