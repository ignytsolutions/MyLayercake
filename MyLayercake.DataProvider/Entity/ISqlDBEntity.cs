using MyLayercake.DataProvider.Entity.Attributes.Properties;
using System;

namespace MyLayercake.DataProvider.Entity {

    public interface ISqlDBEntity : IEntity {
        [IsPrimaryKey]
        Guid Oid { get; set; }
    }
}
