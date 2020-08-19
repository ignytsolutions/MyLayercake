using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using MyLayercake.BusinessObjects.Datalayer.Enums;
using System.Data.SqlClient;

namespace MyLayercake.BusinessObjects.Datalayer.Provider {
    internal class DataProvider : IDisposable {
        #region Privates
        private readonly DbProviderFactory _factory;
        private IDbConnection _connection = null;
        private IDbCommand _command = null;
        private SqlBulkCopy _sqlBulkCopy = null;
        private string _paramPrefix;
        private DBType _dbType;
        private ProviderType _providerType;
        private List<DataProviderParameter> _dataProviderParameters;
        #endregion

        #region Properties
        public string CommandName { get; set; } = string.Empty;

        public Connection ConnectionObject { get; private set; }

        public string Provider { get; private set; }

        public ProviderType ProviderType {
            get {
                return this._providerType;
            }
            set {
                this._providerType = value;
                this.SetDBType();
            }
        }

        public int ParameterCount {
            get {
                return this._command.Parameters.Count;
            }
        }
        #endregion

        #region Constructor
        public DataProvider() { }

        internal DataProvider(Connection ConnectionObject, string CommandName, ProviderType ProviderType) {
            this.ConnectionObject = ConnectionObject ?? throw new DataProviderException("Connection Object cannot be empty");
            this.CommandName = CommandName ?? throw new DataProviderException("Command Name cannot be empty");
            this.ProviderType = ProviderType;

            _factory = DbProviderFactories.GetFactory(this.Provider);
        }
        #endregion

        #region Add Parameter Methods
        private void AddDataProviderParameter(string Name) {
            if (_dataProviderParameters == null)
                _dataProviderParameters = new List<DataProviderParameter>();

            _dataProviderParameters.Add(new DataProviderParameter() { Name = Name });
        }

        private void AddDataProviderParameter(string Name, DbType DbType, object Val) {
            if (_dataProviderParameters == null)
                _dataProviderParameters = new List<DataProviderParameter>();

            _dataProviderParameters.Add(new DataProviderParameter() { Name = Name, DbType = DbType, Val = Val });
        }

        private void AddDataProviderParameter(string Name, object Val) {
            if (_dataProviderParameters == null)
                _dataProviderParameters = new List<DataProviderParameter>();

            _dataProviderParameters.Add(new DataProviderParameter() { Name = Name, Val = Val });
        }

        private void AddDataProviderOutParameter(string Name, DbType DbType) {
            if (_dataProviderParameters == null)
                _dataProviderParameters = new List<DataProviderParameter>();

            _dataProviderParameters.Add(new DataProviderParameter() { Name = Name, DbType = DbType, Output = true });
        }

        private void AddParameter(string Name, DbType DbType, object Val,bool Output) {
            IDataParameter param = this._command.CreateParameter();
            param.ParameterName = Name;

            param.DbType = DbType;

            if (Val != null) {
                param.Value = Val;
            } else {
                param.Value = DBNull.Value;
            }

            if(Output)
                param.Direction = ParameterDirection.Output;

            this._command.Parameters.Add(param);
        }

        public void AddParam(string[] Names, object[] Vals) {
            for (int i = 0;i < Names.Length;i++) {
                AddDataProviderParameter(this._paramPrefix + Names[i], Vals[i]);
            }
        }

        public void AddParam(string Name, string Val) {
            AddDataProviderParameter(this._paramPrefix + Name, Val);
        }

        public void AddParam(string Name) {
            AddDataProviderParameter(this._paramPrefix + Name);
        }

        public void AddParam(string Name, DbType dbType, object Val) {
            AddDataProviderParameter(this._paramPrefix + Name, dbType, Val);
        }

        public void AddParam(string Name, Nullable<Guid> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Guid, Val);
        }

        public void AddParam(string Name, Nullable<Int16> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Int16, Val);
        }

        public void AddParam(string Name, Int32 Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Int32, Val);
        }

        public void AddParam(string Name, Nullable<Int32> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Int32, Val);
        }

        public void AddParam(string Name, Nullable<Int64> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Int64, Val);
        }

        public void AddParam(string Name, Nullable<decimal> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Decimal, Val);
        }

        public void AddParam(string Name, Nullable<double> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Double, Val);
        }

        public void AddParam(string Name, Nullable<float> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Double, Val);
        }

        public void AddParam(string Name, Nullable<DateTime> Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.DateTime, Val);
        }

        public void AddParam(string Name, DateTime Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.DateTime, Val);
        }

        public void AddParam(string Name, DateTimeOffset Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.DateTimeOffset, Val);
        }

        public void AddParam(string Name, bool Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Boolean, Val);
        }

        public void AddParam(string Name, byte[] Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Binary, Val);
        }

        public void AddParam(string Name, object Val) {
            AddDataProviderParameter(this._paramPrefix + Name, DbType.Int64, Val);
        }

        public void AddParamOutput(string Name, DbType DbType) {
            AddDataProviderOutParameter(this._paramPrefix + Name, DbType);
        }
        #endregion

        #region Private Methods
        private void CreateCommand() {
            try {
                if (_providerType == ProviderType.SQLBulkCopy) {
                    this._sqlBulkCopy = new SqlBulkCopy(BuildConnectionString());
                    this._command.Connection = this._connection;

                    this._sqlBulkCopy.DestinationTableName = CommandName;
                    this._sqlBulkCopy.ColumnMappings.Clear();
                    this._sqlBulkCopy.BulkCopyTimeout = 900;
                } else {
                    this._command = this._connection.CreateCommand();
                    this._command.Connection = this._connection;

                    this._command.CommandTimeout = 900;
                    this._command.CommandText = this.CommandName;

                    if (_providerType == ProviderType.StoredProcedure)
                        this._command.CommandType = CommandType.StoredProcedure;
                }
            } catch (Exception) {
                throw;
            }
        }

        private string BuildConnectionString() {
            DbConnectionStringBuilder builder = new DbConnectionStringBuilder {
                ["Data Source"] = this.ConnectionObject.DataSource,
                ["Initial Catalog"] = this.ConnectionObject.Database
            };

            if (this.ConnectionObject.IntergratedSecurity) {
                builder["Integrated Security"] = this.ConnectionObject.IntergratedSecurity;
            } else {
                builder["User ID"] = this.ConnectionObject.UserID;
                builder["Password"] = this.ConnectionObject.Password;
            }
            builder["Application Name"] = this.ConnectionObject.ApplicationName;

            return builder.ConnectionString;
        }

        // Open a connection
        private void OpenConnection() {
            if (_connection == null) {
                _connection = _factory.CreateConnection();
                _connection.ConnectionString = BuildConnectionString();
                _connection.Open();
            }
        }

        // Close a previously opened connection
        private void CloseConnection() {
            if (_connection != null) {
                _connection.Dispose();
                _connection = null;
            }
        }

        private void SetDBType() {
            string dbtype = (_factory == null ? _connection.GetType() : _factory.GetType()).Name;

            if (dbtype.StartsWith("MySql")) _dbType = DBType.MySql;
            else if (dbtype.StartsWith("SqlCe")) _dbType = DBType.SqlServerCE;
            else if (dbtype.StartsWith("Npgsql")) _dbType = DBType.PostgreSQL;
            else if (dbtype.StartsWith("Oracle")) _dbType = DBType.Oracle;
            else if (dbtype.StartsWith("SQLite")) _dbType = DBType.SQLite;

            if (_dbType == DBType.MySql && this.ConnectionObject != null && this.ConnectionObject.AllowUserVariables == true) {
                _paramPrefix = "?";
            } else if (_dbType == DBType.Oracle) {
                _paramPrefix = ":";
            } else {
                _paramPrefix = "@";
            }
        }

        private void AddParameters() {
            try {
                if (_dataProviderParameters != null) {
                    foreach (DataProviderParameter dataProviderParameter in _dataProviderParameters) {
                        AddParameter(dataProviderParameter.Name, dataProviderParameter.DbType, dataProviderParameter.Val, dataProviderParameter.Output);
                    }
                }
            } catch (Exception) {
                throw;
            }
        }

        private void OpenAndCreate() {
            try {
                this.OpenConnection();
                this.CreateCommand();
                this.AddParameters();
            } catch (Exception) {
                throw;
            }
        }

        private static IEnumerable<IDataParameter> GetParameters<T>(IDbCommand command, T objParam) {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            if (objParam != null) {
                PropertyInfo[] properties = typeof(T).GetProperties();
                SqlCommandBuilder.DeriveParameters((SqlCommand)command);
                // Initialize Index of parameterValues Array
                int index = 0;

                // Populate the Input Parameters With Values Provided        
                foreach (SqlParameter parameter in command.Parameters) {
                    //if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                    //{
                    foreach (PropertyInfo property in properties) {
                        if (("@" + property.Name.ToLower()) == parameter.ParameterName.ToLower()) {
                            if (property.GetValue(objParam, null) != null) {
                                parameter.Value = property.GetValue(objParam, null);
                            } else {
                                parameter.Value = DBNull.Value;
                            }
                            collection.Add(parameter);
                            break;
                        }
                        index++;
                    }
                    index = 0;
                    //}
                }

                command.Parameters.Clear();
            }
            return collection;
        }

        private static IEnumerable<IDataParameter> GetParameters(IDbCommand command, object objParam) {
            Collection<IDataParameter> collection = new Collection<IDataParameter>();
            if (objParam != null) {
                PropertyInfo[] properties = objParam.GetType().GetProperties();
                SqlCommandBuilder.DeriveParameters((SqlCommand)command);
                // Initialize Index of parameterValues Array
                int index = 0;

                // Populate the Input Parameters With Values Provided        
                foreach (SqlParameter parameter in command.Parameters) {
                    //if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                    //{
                    foreach (PropertyInfo property in properties) {
                        if (("@" + property.Name.ToLower()) == parameter.ParameterName.ToLower()) {
                            if (property.GetValue(objParam, null) != null) {
                                parameter.Value = property.GetValue(objParam, null);
                            } else {
                                parameter.Value = DBNull.Value;
                            }
                            collection.Add(parameter);
                            break;
                        }
                        index++;
                    }
                    index = 0;
                    //}
                }

                command.Parameters.Clear();
            }
            return collection;
        }
        #endregion

        #region Public Methods
        public void Execute() {
            try {
                this.OpenAndCreate();
                this._command.ExecuteNonQuery();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public object ExecuteOutputParameter(string parameterName) {
            try {
                this.OpenAndCreate();
                this._command.ExecuteNonQuery();
                object outputParameter = (_command.Parameters as DbParameterCollection)["@" + parameterName].Value;
                return outputParameter;
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public object[] ExecuteOutputParameters(string[] parameterNames) {
            try {
                object[] values = new object[parameterNames.Length];

                this.OpenAndCreate();
                this._command.ExecuteNonQuery();

                for (int i = 0; i < parameterNames.Length;i++) {
                    object outputParameter = (_command.Parameters as DbParameterCollection)["@" + parameterNames[i]].Value;
                    values.SetValue(outputParameter, i);
                }

                return values;
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public void ExecuteObject(object Object) {
            try {
                this.OpenAndCreate();

                foreach (IDataParameter param in GetParameters(_command, Object))
                    _command.Parameters.Add(param);

                this._command.ExecuteNonQuery();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public void Execute<T>(T Object) {
            try {
                this.OpenAndCreate();

                foreach (IDataParameter param in GetParameters<T>(_command, Object))
                    _command.Parameters.Add(param);

                this._command.ExecuteNonQuery();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public void ExecuteBulkCopy(DataTable DataTable) {
            try {
                this.OpenAndCreate();

                foreach (DataColumn column in DataTable.Columns) {
                    this._sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                this._sqlBulkCopy.WriteToServer(DataTable);
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public void ExecuteBulkCopy(IDataReader DataReader) {
            try {
                this.OpenAndCreate();

                for (int i = 0; i < DataReader.FieldCount; i++) {
                    this._sqlBulkCopy.ColumnMappings.Add(DataReader.GetName(i), DataReader.GetName(i));
                }

                this._sqlBulkCopy.WriteToServer(DataReader);
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public void ExecuteUpdate(Guid ID) {
            try {
                this.OpenAndCreate();

                this._command.CommandText = String.Format("{0} '{1}'", this._command.CommandText, ID);
                this._command.ExecuteNonQuery();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }
        }

        public IDataReader ExecuteReader() {
            IDataReader reader;

            try {
                this.OpenAndCreate();

                reader = this._command.ExecuteReader(CommandBehavior.CloseConnection);
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }

            return reader;
        }

        public DataSet ExecuteDataSet() {
            DataSet dataSet = new DataSet();

            try {
                this.OpenAndCreate();

                IDbDataAdapter adapter = _factory.CreateDataAdapter();
                adapter.SelectCommand = _command;
                adapter.Fill(dataSet);
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }

            return dataSet;
        }

        public IEnumerable<T> ExecuteEnumerable<T>() where T : new() {
            IDataReader reader;
            T tempObject;

            Collection<T> objects = new Collection<T>();

            try {
                this.OpenAndCreate();

                reader = this._command.ExecuteReader();

                while (reader.Read()) {
                    tempObject = new T();

                    for (int i = 0; i < reader.FieldCount; i++) {
                        SetPropValue(reader.GetName(i), tempObject, typeof(T), reader.GetValue(i));
                    }

                    objects.Add(tempObject);
                }

                reader.Close();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }

            return objects;
        }

        private static void SetPropValue(string name, object obj, Type type,object value) {
            var parts = name.Split('.').ToList();
            var currentPart = parts[0];

            PropertyInfo info = type.GetProperty(currentPart);

            if (info == null) { throw new Exception(String.Format("Cannot map database column {0} to object {1}", value, obj.GetType().Name)); ; }

            if (name.IndexOf(".") > -1) {
                parts.Remove(currentPart);
                SetPropValue(string.Join(".", parts), info.GetValue(obj, null), info.PropertyType, value);
            } else {
                if (value == DBNull.Value)
                    value = null;

                info.SetValue(obj, value, null);
            }
        }

        public IEnumerable<T> ExecuteEnumerableFields<T>() where T : new() {
            IDataReader reader;
            T tempObject;

            Collection<T> objects = new Collection<T>();

            try {
                this.OpenAndCreate();

                reader = this._command.ExecuteReader();

                while (reader.Read()) {
                    tempObject = new T();

                    for (int i = 0; i < reader.FieldCount; i++) {
                        FieldInfo fieldInfo = tempObject.GetType().GetField(reader.GetName(i));

                        if (fieldInfo != null) {
                            object value = reader.GetValue(i);

                            if (value == DBNull.Value)
                                value = null;

                            fieldInfo.SetValue(tempObject, value);
                        } else {
                            throw new Exception(String.Format("Cannot map database column {0} to object {1}", reader.GetName(i), tempObject.GetType().Name));
                        }
                    }

                    objects.Add(tempObject);
                }

                reader.Close();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }

            return objects;
        }

        public object ExecuteScalar() {
            object obj;

            try {
                this.OpenAndCreate();

                obj = this._command.ExecuteScalar();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }

            return obj;
        }

        public T ExecuteScalar<T>() where T : new() {
            T obj;

            try {
                this.OpenAndCreate();

                obj = (T)this._command.ExecuteScalar();
            } catch (Exception) {
                throw;
            } finally {
                _dataProviderParameters = null;
            }

            return obj;
        }
        #endregion

        #region Private Poco Methods

        #endregion

        #region Dispose
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (_connection != null) {
                    _connection.Dispose();
                    _connection = null;
                }
                if (_command != null) {
                    _command.Dispose();
                    _command = null;
                }
            }

        }

        ~DataProvider() {
            Dispose(false);
        }
        #endregion
    }
}