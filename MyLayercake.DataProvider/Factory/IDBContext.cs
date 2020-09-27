using System;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public interface IDBContext : IDisposable {
        IDatabaseSettings IDatabaseSettings { get; }
        DataProvider Instance { get; }
    }
}
