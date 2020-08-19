using MyLayercake.BusinessObjects.Datalayer.Provider;
using System;
using System.Collections.Generic;

namespace MyLayercake.BusinessObjects.Logic {
    public sealed class RepositorySingleton<T> : RepositoryBase<T> where T : IEntity, new() {
        private static volatile RepositorySingleton<T> _instance;
        private readonly static object _syncRoot = new object();

        internal RepositorySingleton() { }
        internal RepositorySingleton(Connection ConnectionObject) : base(ConnectionObject) { }

        public static RepositorySingleton<T> GetInstance {
            get {
                if (_instance == null) {
                    lock (_syncRoot) {
                        if (_instance == null) {
                            _instance = new RepositorySingleton<T>();
                        }
                    }
                }

                return _instance;
            }
        }

        public static RepositorySingleton<T> SetInstance(Connection ConnectionObject) {
            if (_instance == null) {
                lock (_syncRoot) {
                    if (_instance == null) {
                        _instance = new RepositorySingleton<T>(ConnectionObject);
                    }
                }
            }

            return _instance;
        }
    }
}