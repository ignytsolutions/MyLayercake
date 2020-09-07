﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MyLayercake.Sql.Result {
	/// <summary>
	/// Represents <see cref="IDataReader"/> result that can be mapped to POCO model, dictionary, <see cref="RecordSet"/> or <see cref="DataTable"/>.
	/// </summary>
	public class DataReaderResult : IQueryModelResult, IQueryDictionaryResult, IQueryRecordSetResult, IQueryDataTableResult {
		readonly IDataReader DataReader;
		readonly int RecordOffset;
		readonly int RecordCount;
        readonly DataMapper DtoMapper;
		Func<IDataReaderMapperContext, object> CustomMappingHandler = null;
        readonly string FirstFieldName = null;

		/// <summary>
		/// Initializes a new instance of the DbDataAdapter with specified <see cref="IDataReader"/> instance.
		/// </summary>
		/// <param name="dataReader">data reader instance</param>
		public DataReaderResult(IDataReader dataReader) : this(dataReader, 0, Int32.MaxValue) {
		}

		/// <summary>
		/// Initializes a new instance of the DbDataAdapter with specified <see cref="IDataReader"/> instance.
		/// </summary>
		/// <param name="dataReader">data reader instance</param>
		/// <param name="offset">first record offset</param>
		/// <param name="count">max number of records to read</param>
		public DataReaderResult(IDataReader dataReader, int offset, int count) {
			DataReader = dataReader;
			RecordOffset = offset;
			RecordCount = count;
			DtoMapper = DataMapper.Instance;
		}

		internal DataReaderResult(IDataReader dataReader, int offset, int count, string firstField) : this(dataReader, offset, count) {
			FirstFieldName = firstField;
		}

		/// <summary>
		/// Configures custom mapping handler for POCO models.
		/// </summary>
		public DataReaderResult SetMapper(Func<IDataReaderMapperContext, object> handler) {
			CustomMappingHandler = handler;
			return this;
		}

		/// <summary>
		/// Returns the first record from the query result. 
		/// </summary>
		/// <returns>depending on T, single value or all fields values from the first record</returns>
		public T Single<T>() {
			var res = new SingleDataReaderResult<T>(Read<T>);
			ExecuteReader(res, 1);
			return res.Result;
		}

		/// <summary>
		/// Asynchronously returns the first record from the query result. 
		/// </summary>
		/// <returns>depending on T, single value or all fields values from the first record</returns>
		public Task<T> SingleAsync<T>() {
			return SingleAsync<T>(CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously returns the first record from the query result. 
		/// </summary>
		/// <returns>depending on T, single value or all fields values from the first record</returns>
		public Task<T> SingleAsync<T>(CancellationToken cancel) {
			return ExecuteReaderAsync<T>(
				new SingleDataReaderResult<T>(Read<T>), 1, cancel
			);
		}

		/// <summary>
		/// Returns a list with all query results.
		/// </summary>
		/// <returns>list with query results</returns>
		public List<T> ToList<T>() {
			var res = new ListDataReaderResult<T>(Read<T>);
			ExecuteReader(res, RecordCount);
			return res.Result;
		}

		/// <summary>
		/// Asynchronously returns a list with all query results.
		/// </summary>
		public Task<List<T>> ToListAsync<T>() {
			return ToListAsync<T>(CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously returns a list with all query results.
		/// </summary>
		public Task<List<T>> ToListAsync<T>(CancellationToken cancel) {
			return ExecuteReaderAsync<List<T>>(
				new ListDataReaderResult<T>(Read<T>), RecordCount, cancel
			);
		}

		/// <summary>
		/// Returns dictionary with first record values.
		/// </summary>
		/// <returns>dictionary with field values or null if query returns zero records.</returns>
		public Dictionary<string, object> ToDictionary() {
			var res = new SingleDataReaderResult<Dictionary<string, object>>(ReadDictionary);
			ExecuteReader(res, 1);
			return res.Result;
		}

		/// <summary>
		/// Asynchronously returns dictionary with first record values.
		/// </summary>
		public Task<Dictionary<string, object>> ToDictionaryAsync() {
			return ToDictionaryAsync(CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously returns dictionary with first record values.
		/// </summary>
		public Task<Dictionary<string, object>> ToDictionaryAsync(CancellationToken cancel) {
			return ExecuteReaderAsync<Dictionary<string, object>>(
				new SingleDataReaderResult<Dictionary<string, object>>(ReadDictionary), 1, cancel
			);
		}


		/// <summary>
		/// Returns a list of dictionaries with all query results.
		/// </summary>
		public List<Dictionary<string, object>> ToDictionaryList() {
			return ToList<Dictionary<string, object>>();
		}

		/// <summary>
		/// Asynchronously a list of dictionaries with all query results.
		/// </summary>
		public Task<List<Dictionary<string, object>>> ToDictionaryListAsync() {
			return ToDictionaryListAsync(CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously a list of dictionaries with all query results.
		/// </summary>
		public Task<List<Dictionary<string, object>>> ToDictionaryListAsync(CancellationToken cancel) {
			return ExecuteReaderAsync<List<Dictionary<string, object>>>(
				new ListDataReaderResult<Dictionary<string, object>>(ReadDictionary), RecordCount, cancel
			);
		}



		/// <summary>
		/// Returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		public RecordSet ToRecordSet() {
			var res = new RecordSetDataReaderResult();
			ExecuteReader(res, RecordCount);
			return res.Result;
		}

		/// <summary>
		/// Asynchronously returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		public Task<RecordSet> ToRecordSetAsync() {
			return ToRecordSetAsync(CancellationToken.None);
		}

		/// <summary>
		/// Asynchronously returns all query results as <see cref="RecordSet"/>.
		/// </summary>
		public Task<RecordSet> ToRecordSetAsync(CancellationToken cancel) {
			return ExecuteReaderAsync<RecordSet>(new RecordSetDataReaderResult(), RecordCount, cancel);
		}

		/// <summary>
		/// Returns all query results as <see cref="DataTable"/>.
		/// </summary>
		public DataTable ToDataTable() => ToDataTable(null);

		/// <summary>
		/// Loads all query results into specified <see cref="DataTable"/>.
		/// </summary>
		/// <remarks>
		/// Columns mapping is not supported; you can generate SELECT command with <see cref="DbCommandBuilder"/> and use
		/// it with <see cref="System.Data.Common.DbDataAdapter"/> implementation if you need full support of <see cref="DataTable"/> features.
		/// </remarks>
		public DataTable ToDataTable(DataTable tbl) {
			var res = new DataTableDataReaderResult(tbl ?? new DataTable());
			ExecuteReader(res, RecordCount);
			return res.Result;
		}

		/// <summary>
		/// Loads all query results into specified <see cref="DataTable"/>.
		/// </summary>
		public Task<DataTable> ToDataTableAsync(CancellationToken cancel = default(CancellationToken))
			=> ToDataTableAsync(null, cancel);

		/// <summary>
		/// Loads all query results into specified <see cref="DataTable"/>.
		/// </summary>
		public Task<DataTable> ToDataTableAsync(DataTable tbl, CancellationToken cancel = default(CancellationToken)) {
			return ExecuteReaderAsync<DataTable>(new DataTableDataReaderResult(tbl ?? new DataTable()), RecordCount, cancel);
		}

		private T ChangeType<T>(object o, TypeCode typeCode) {
			if (DataHelper.IsNullOrDBNull(o)) {
				return default(T);
			}
			return (T)Convert.ChangeType(o, typeCode, System.Globalization.CultureInfo.InvariantCulture);
		}

		private Dictionary<string, object> ReadDictionary(IDataReader rdr) {
			var dictionary = new Dictionary<string, object>(rdr.FieldCount);
			for (int i = 0; i < rdr.FieldCount; i++)
				dictionary[rdr.GetName(i)] = rdr.GetValue(i);
			return dictionary;
		}

		private T Read<T>(IDataReader rdr) {
			var undelyingType = Nullable.GetUnderlyingType(typeof(T));
			var typeCode = Type.GetTypeCode(undelyingType ?? typeof(T));
			// handle primitive single-value result
			if (typeCode != TypeCode.Object || typeof(T) == typeof(object)) {
				if (rdr.FieldCount == 1) {
					return ChangeType<T>(rdr[0], typeCode);
				} else if (rdr.FieldCount > 1) {
					var firstFld = FirstFieldName;
					var val = firstFld != null ? rdr[firstFld] : rdr[0];
					return ChangeType<T>(val, typeCode);
				} else {
					return default(T);
				}
			}
			// T is a dto
			// special handling for dictionaries
			var type = typeof(T);
			if (type == typeof(IDictionary) || type == typeof(IDictionary<string, object>) || type == typeof(Dictionary<string, object>)) {
				return (T)((object)ReadDictionary(rdr));
			}
			// handle as poco model
			if (CustomMappingHandler != null) {
				var mappingContext = new DataReaderMapperContext(DtoMapper, rdr, type);
				var mapResult = CustomMappingHandler(mappingContext);
				if (mapResult == null)
					throw new NullReferenceException("Custom mapping handler returns null");
				if (!(mapResult is T))
					throw new InvalidCastException($"Custom mapping handler returns incompatible object type '{mapResult.GetType()}' (expected '{type}')");
				return (T)mapResult;
			}
			return DtoMapper.MapTo<T>(rdr);
		}

		void ExecuteReader<T>(IDataReaderResult<T> result, int recordCount) {
			int index = 0;
			int processed = 0;
			result.Init(DataReader);
			while (DataReader.Read() && processed < recordCount) {
				if (index >= RecordOffset) {
					processed++;
					result.Read(DataReader);
				}
				index++;
			}
			result.End();
		}

		async Task<T> ExecuteReaderAsync<T>(IDataReaderResult<T> result, int recordCount, CancellationToken cancel) {
			int index = 0;
			int processed = 0;

			result.Init(DataReader);
			while ((await DataReader.ReadAsync(cancel)) && processed < recordCount) {
				if (index >= RecordOffset) {
					processed++;
					result.Read(DataReader);
				}
				index++;
			}
			result.End();
			return result.Result;
		}

	}
}
