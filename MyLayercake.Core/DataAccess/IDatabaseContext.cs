using System;
using System.Data;
using System.Data.Common;

namespace MyLayercake.Core.DataAccess {
    public interface IDatabaseContext : IDisposable {
        IDbConnection Connection { get; }
        DbProviderFactory DbProviderFactory { get; }
    }
}
