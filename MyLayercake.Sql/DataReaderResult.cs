using System;
using System.Collections.Generic;
using System.Data;

namespace MyLayercake.Sql {
	internal interface IDataReaderResult<T> {
		T Result { get; }
		void Init(IDataReader rdr);
		void Read(IDataReader rdr);
		void End();
	}

	internal class SingleDataReaderResult<T> : IDataReaderResult<T> {
		public T Result { get; private set; }

		private readonly Func<IDataReader, T> Convert;

		internal SingleDataReaderResult(Func<IDataReader, T> convert) {
			Convert = convert;
			Result = default;
		}

		public void Init(IDataReader rdr) { }
		public void End() { }

		public void Read(IDataReader rdr) {
			Result = Convert(rdr);
		}
	}

	internal class ListDataReaderResult<T> : IDataReaderResult<List<T>> {
		public List<T> Result { get; private set; }

		private readonly Func<IDataReader, T> Convert;

		internal ListDataReaderResult(Func<IDataReader, T> convert) {
			Convert = convert;
			Result = new List<T>();
		}

		public void Init(IDataReader rdr) { }
		public void End() { }

		public void Read(IDataReader rdr) {
			Result.Add(Convert(rdr));
		}
	}

	internal class RecordSetDataReaderResult : IDataReaderResult<RecordSet> {
		public RecordSet Result { get; private set; }

		internal RecordSetDataReaderResult() {
			Result = null;
		}

		public void Init(IDataReader rdr) {
			if (Result == null) {
				Result = DataHelper.GetRecordSetByReader(rdr);
			}
		}
		public void End() { }

		public void Read(IDataReader rdr) {
			var rowValues = new object[rdr.FieldCount];
			rdr.GetValues(rowValues);
			Result.Add(rowValues).AcceptChanges();
		}
	}

	internal class DataTableDataReaderResult : IDataReaderResult<DataTable> {
		public DataTable Result { get; private set; }

		int[] RdrIdxToTblIdx;

		internal DataTableDataReaderResult(DataTable res) {
			Result = res;
		}

		public void Init(IDataReader rdr) {
			DataHelper.EnsureDataTableColumnsByReader(Result, rdr);

			var tblColToIdx = new Dictionary<string, int>(Result.Columns.Count);
			for (int i = 0; i < Result.Columns.Count; i++)
				tblColToIdx[Result.Columns[i].ColumnName] = i;

			RdrIdxToTblIdx = new int[rdr.FieldCount];
			for (int i = 0; i < rdr.FieldCount; i++) {
				RdrIdxToTblIdx[i] = tblColToIdx[rdr.GetName(i)];
			}

			Result.BeginLoadData();
		}

		public void Read(IDataReader rdr) {
			var rowValues = new object[Result.Columns.Count];
			for (int i = 0; i < rdr.FieldCount; i++) {
				rowValues[RdrIdxToTblIdx[i]] = rdr.GetValue(i);
			}
			Result.LoadDataRow(rowValues, true);
		}

		public void End() {
			Result.EndLoadData();
		}
	}
}
