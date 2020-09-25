using System;

namespace MyLayercake.Core.DataAccess {
    public interface IDatabaseContextFactory : IDisposable {
        IDatabaseContext Context { get; }
    }
}
