using System;
using System.Data;

namespace MyLayercake.Sql {
	public partial class DbDataAdapter {
		/// <summary>
		/// Represents <see cref="DbDataAdapter.SelectQuery"/> context for custom data mapping to POCO models.
		/// </summary>
		public interface IMapperContext {
			/// <summary>
			/// Data reader with current record's data.
			/// </summary>
			IDataReader DataReader { get; }

			/// <summary>
			/// Target POCO model type.
			/// </summary>
			Type ObjectType { get; }

			/// <summary>
			/// Performs default data mapping to specified object (data annotations are used if present).
			/// </summary>
			void MapTo(object o);

			/// <summary>
			/// Creates model of specified type and performs default mapping to this object.
			/// </summary>
			object MapTo(Type t);
		}

		internal sealed class MapperContext : IMapperContext {
			public IDataReader DataReader { get; private set; }

			public Type ObjectType { get; private set; }

			private readonly DataMapper Mapper;

			internal MapperContext(DataMapper mapper, IDataReader rdr, Type toType) {
				Mapper = mapper;
				DataReader = rdr;
				ObjectType = toType;
			}

			public void MapTo(object o) {
				var t = o.GetType();
				var schema = Mapper.GetSchema(t);
				Mapper.MapTo(DataReader, o, t, schema);
			}

			public object MapTo(Type t) {
				return Mapper.MapTo(DataReader, t);
			}
		}
	}
}
