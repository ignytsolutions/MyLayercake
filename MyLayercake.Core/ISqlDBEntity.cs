using MyLayercake.Core.Attributes;
using System;

namespace MyLayercake.Core {

    public interface ISqlDBEntity : IEntity {
        [Key]
        Guid Oid { get; set; }
    }
}
