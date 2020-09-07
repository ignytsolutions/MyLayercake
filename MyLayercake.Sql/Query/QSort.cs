using System;
using System.ComponentModel;

namespace MyLayercake.Sql.Query {
	/// <summary>
	/// Represents query sort option. 
	/// </summary>
	public class QSort {
		public const string Asc = "asc";
		public const string Desc = "desc";

		/// <summary>
		/// Get QField sort target
		/// </summary>
		public QField Field { get; private set; }

		/// <summary>
		/// Get sort direction (asc or desc)
		/// </summary>
		public ListSortDirection SortDirection { get; private set; }

		/// <summary>
		/// Initializes a new instance of the QSoft with specified field name
		/// </summary>
		/// <param name="sortFld">field name with optional direction suffix like "id desc"</param>
		public QSort(string sortFld) {
			SortDirection = ListSortDirection.Ascending;

			sortFld = sortFld.Trim();
			int lastSpaceIdx = sortFld.LastIndexOf(' ');
			string lastWord = lastSpaceIdx != -1 ? sortFld.Substring(lastSpaceIdx + 1).ToLower() : null;
			bool sortDirectionFound = true;

			if (lastWord == Asc || lastWord == "ascending")
				SortDirection = ListSortDirection.Ascending;
			else if (lastWord == Desc || lastWord == "descending")
				SortDirection = ListSortDirection.Descending;
			else
				sortDirectionFound = false;

			Field = new QField(sortDirectionFound ? sortFld.Substring(0, lastSpaceIdx).TrimEnd() : sortFld);
			if (Field.Name == String.Empty)
				throw new ArgumentException("Invalid sort field");
		}

		/// <summary>
		/// Initializes a new instance of the QSoft with specified field name and sort direction
		/// </summary>
		/// <param name="fld">field name</param>
		/// <param name="direction">sort direction</param>
		public QSort(string fld, ListSortDirection direction) {
			Field = (QField)fld;
			SortDirection = direction;
		}

		/// <summary>
		/// Returns a string representation of QSort
		/// </summary>
		/// <returns>string in [field name] [asc|desc] format</returns>
		public override string ToString() {
			return String.Format("{0} {1}", Field.ToString(), SortDirection == ListSortDirection.Ascending ? Asc : Desc);
		}

		public static implicit operator QSort(string value) {
			return new QSort(value);
		}

		public static implicit operator string(QSort value) {
			return value.ToString();
		}
	}
}
