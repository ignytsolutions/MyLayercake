using System;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.Core {
    public interface IEntity {
        DateTime Created { get; set; }
    }
}
