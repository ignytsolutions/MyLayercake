using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MyLayercake.Core.Extensions {

    internal static class IDbCommandExtentions {
        private static Dictionary<Type, DbType> _typeMap;
        private static Dictionary<Type, DbType> TypeMap {
            get {
                if (_typeMap == null)
                    _typeMap = new Dictionary<Type, DbType>(37) {
                        [typeof(byte)] = DbType.Byte,
                        [typeof(sbyte)] = DbType.SByte,
                        [typeof(short)] = DbType.Int16,
                        [typeof(ushort)] = DbType.UInt16,
                        [typeof(int)] = DbType.Int32,
                        [typeof(uint)] = DbType.UInt32,
                        [typeof(long)] = DbType.Int64,
                        [typeof(ulong)] = DbType.UInt64,
                        [typeof(float)] = DbType.Single,
                        [typeof(double)] = DbType.Double,
                        [typeof(decimal)] = DbType.Decimal,
                        [typeof(bool)] = DbType.Boolean,
                        [typeof(string)] = DbType.String,
                        [typeof(char)] = DbType.StringFixedLength,
                        [typeof(Guid)] = DbType.Guid,
                        [typeof(DateTime)] = DbType.DateTime,
                        [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                        [typeof(TimeSpan)] = DbType.Time,
                        [typeof(byte[])] = DbType.Binary,
                        [typeof(byte?)] = DbType.Byte,
                        [typeof(sbyte?)] = DbType.SByte,
                        [typeof(short?)] = DbType.Int16,
                        [typeof(ushort?)] = DbType.UInt16,
                        [typeof(int?)] = DbType.Int32,
                        [typeof(uint?)] = DbType.UInt32,
                        [typeof(long?)] = DbType.Int64,
                        [typeof(ulong?)] = DbType.UInt64,
                        [typeof(float?)] = DbType.Single,
                        [typeof(double?)] = DbType.Double,
                        [typeof(decimal?)] = DbType.Decimal,
                        [typeof(bool?)] = DbType.Boolean,
                        [typeof(char?)] = DbType.StringFixedLength,
                        [typeof(Guid?)] = DbType.Guid,
                        [typeof(DateTime?)] = DbType.DateTime,
                        [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                        [typeof(TimeSpan?)] = DbType.Time,
                        [typeof(object)] = DbType.Object
                    };

                return _typeMap;
            }
        }

        private static readonly string _parameterPrefix = "@";

        #region Add Parameter
        public static void AddParameter(this IDbCommand command, string name, DbType dbType, object val, bool output) {
            IDataParameter param = command.CreateParameter();
            param.ParameterName = string.Concat(_parameterPrefix, name);
            param.DbType = dbType;

            if (val != null) {
                param.Value = val;
            } else {
                param.Value = DBNull.Value;
            }

            if (output)
                param.Direction = ParameterDirection.Output;

            command.Parameters.Add(param);
        }

        public static void AddParameter(this IDbCommand command, string name, DbType dbType, object val) {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = string.Concat(_parameterPrefix, name);
            param.DbType = dbType;

            if (val != null) {
                param.Value = val;
            } else {
                param.Value = DBNull.Value;
            }

            command.Parameters.Add(param);
        }

        public static void AddParam(this IDbCommand command, string name, object value, Type type) {
            AddParameter(command, name, TypeMap[type], value);
        }

        public static void AddParam(this IDbCommand command, string[] Names, object[] Vals) {
            for (int i = 0; i < Names.Length; i++) {
                AddParameter(command, Names[i], TypeMap[Vals[i].GetType()], Vals[i]);
            }
        }

        public static void AddParam(this IDbCommand command, string name, string val) {
            AddParameter(command, name, TypeMap[typeof(string)], val);
        }

        public static void AddParam(this IDbCommand command, string name, DbType dbType, object val) {
            AddParameter(command, name, dbType, val);
        }

        public static void AddParam(this IDbCommand command, string name, Guid? val) {
            AddParameter(command, name, TypeMap[typeof(Guid)], val);
        }

        public static void AddParam(this IDbCommand command, string name, byte? val) {
            AddParameter(command, name, TypeMap[typeof(byte)], val);
        }

        public static void AddParam(this IDbCommand command, string name, short? val) {
            AddParameter(command, name, TypeMap[typeof(short)], val);
        }

        public static void AddParam(this IDbCommand command, string name, int? val) {
            AddParameter(command, name, TypeMap[typeof(int)], val);
        }

        public static void AddParam(this IDbCommand command, string name, long? val) {
            AddParameter(command, name, TypeMap[typeof(long)], val);
        }

        public static void AddParam(this IDbCommand command, string name, decimal? val) {
            AddParameter(command, name, TypeMap[typeof(decimal)], val);
        }

        public static void AddParam(this IDbCommand command, string name, double? val) {
            AddParameter(command, name, TypeMap[typeof(double)], val);
        }

        public static void AddParam(this IDbCommand command, string name, float? val) {
            AddParameter(command, name, TypeMap[typeof(float)], val);
        }

        public static void AddParam(this IDbCommand command, string name, DateTime? val) {
            AddParameter(command, name, TypeMap[typeof(DateTime)], val);
        }

        public static void AddParam(this IDbCommand command, string name, DateTimeOffset val) {
            AddParameter(command, name, TypeMap[typeof(DateTimeOffset)], val);
        }

        public static void AddParam(this IDbCommand command, string name, bool val) {
            AddParameter(command, name, TypeMap[typeof(bool)], val);
        }

        public static void AddParam(this IDbCommand command, string name, byte[] val) {
            AddParameter(command, name, TypeMap[typeof(byte[])], val);
        }

        public static void AddParam(this IDbCommand command, string name, object val) {
            AddParameter(command, name, TypeMap[typeof(object)], val);
        }
        #endregion

        #region Create Crud Parameters
        public static void AddDeleteParamaters<T>(this IDbCommand command, T domainObject) {
            command.AddParam(domainObject.PrimaryKeyName(), domainObject.PrimaryKeyValue(), domainObject.PrimaryKeyType());
        }

        public static void AddInsertParamaters<T>(this IDbCommand command, T domainObject) {
            foreach (PropertyInfo property in domainObject.Properties()) {
                command.AddParam(property.Name, property.GetValue(domainObject), property.PropertyType);
            }
        }

        public static void AddUpdateParamaters<T>(this IDbCommand command, T domainObject) {
            foreach (PropertyInfo property in domainObject.Properties()) {
                command.AddParam(property.Name, property.GetValue(domainObject), property.PropertyType);
            }
        }

        public static void AddSelectByIDParamaters<T>(this IDbCommand command, object Oid) where T : IEntity, new() {
            T obj = new T();

            command.AddParam(obj.PrimaryKeyName(), Oid, obj.PrimaryKeyType());
        }
        #endregion
    }
}