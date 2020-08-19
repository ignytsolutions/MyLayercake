using MyLayercake.BusinessObjects.Datalayer.Enums;
using MyLayercake.BusinessObjects.Datalayer.Provider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyLayercake.BusinessObjects.Datalayer {
    public class DataAccessEventArgs : EventArgs {
        public IEntity ModifiedObject { get; set; }
        public bool Success { get; set; } 
    }

    public abstract class DataAccess {
        public event EventHandler<DataAccessEventArgs> Inserting;
        public event EventHandler<DataAccessEventArgs> Inserted;
        public event EventHandler<DataAccessEventArgs> Updating;
        public event EventHandler<DataAccessEventArgs> Updated;
        public event EventHandler<DataAccessEventArgs> Deleting;
        public event EventHandler<DataAccessEventArgs> Deleted;

        private List<IEntity> _objects = new List<IEntity>();

        public Connection ConnectionObject { get; set; }

        #region Constructors
        protected DataAccess() {}

        protected DataAccess(Connection ConnectionObject) {
            this.ConnectionObject = ConnectionObject;
        }
        #endregion

        #region Protected Methods
        protected IEnumerable<T> Get<T>() where T : IEntity, new() {
            using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().SelectProcedureName(), ProviderType.StoredProcedure);

            return provider.ExecuteEnumerable<T>();
        }

        protected IEnumerable<T> Get<T>(IEnumerable<T> Objects) where T : IEntity, new() {
            List<T> returnData = new List<T>();

            using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().SelectProcedureName(), ProviderType.StoredProcedure);

            foreach (var obj in Objects) {
                provider.AddParam(obj.PrimaryKeyName<T>(), obj.PrimaryKeyValue<T>());
                IEnumerable<T> returnedData = provider.ExecuteEnumerable<T>();

                returnData.AddRange(returnedData);
            }

            return returnData;
        }

        protected IEnumerable<T> Get<T>(int ID) where T : IEntity, new() {
            using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().SelectByIDProcedureName(), ProviderType.StoredProcedure);

            provider.AddParam(GetObject<T>().PrimaryKeyName(), ID);

            return provider.ExecuteEnumerable<T>();
        }

        protected void Delete<T>(IEnumerable<T> Objects) where T : IEntity, new() {
            if (Objects != null & Objects.Count() >= 1) {
                using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().DeleteProcedureName(), ProviderType.StoredProcedure);
                
                foreach (var obj in Objects) {
                    this.OnDeleting(obj, false);

                    provider.AddParam(obj.PrimaryKeyName<T>(), obj.PrimaryKeyValue<T>());
                    provider.Execute();

                    this.OnDeleted(obj, true);
                }
            }
        }

        protected void Delete<T>(T Object) where T : IEntity, new() {
            using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().DeleteProcedureName(), ProviderType.StoredProcedure);

            this.OnDeleting(Object, false);

            provider.AddParam(Object.PrimaryKeyName<T>(), Object.PrimaryKeyValue<T>());
            provider.Execute();

            this.OnDeleted(Object, true);
        }

        protected void Delete(IEnumerable<IEntity> Objects) {
            try {
                GetCollections(Objects);

                if (_objects != null & _objects.Count() >= 1) {
                    foreach (var obj in _objects) {
                        string storedProcedure = (obj as IEntity).DeleteProcedureName();
                        if (storedProcedure != null) {
                            using DataProvider provider = new DataProvider(this.ConnectionObject, storedProcedure, ProviderType.StoredProcedure);

                            this.OnDeleting(obj, false);

                            provider.AddParam((obj as IEntity).PrimaryKeyName(), (obj as IEntity).PrimaryKeyValue());
                            provider.ExecuteObject(obj);

                            this.OnDeleted(obj, true);
                        }
                    }
                }
            } catch (Exception) {
                throw;
            } finally {
                this._objects = null;
            }
        }

        protected void Insert<T>(IEnumerable<T> Objects) where T : IEntity, new() {
            if (Objects != null & Objects.Count() >= 1) {
                using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().InsertProcedureName(), ProviderType.StoredProcedure);

                foreach (var obj in Objects) {
                    this.OnInserting(obj, false);

                    provider.Execute<T>(obj);

                    this.OnInserted(obj, true);
                }
            }
        }

        protected void Insert(IEnumerable<IEntity> Objects) {
            try {
                GetCollections(Objects);

                if (_objects != null & _objects.Count() >= 1) {
                    foreach (var obj in _objects) {
                        string storedProcedure = (obj as IEntity).InsertProcedureName();

                        this.OnInserting(obj, false);

                        if (storedProcedure != null) {
                            using DataProvider provider = new DataProvider(this.ConnectionObject, storedProcedure, ProviderType.StoredProcedure);

                            provider.ExecuteObject(obj);

                            this.OnInserted(obj, true);
                        }
                    }
                }
            } catch (Exception) {
                throw;
            } finally {
                this._objects = null;
            }
        }

        protected void Update<T>(IEnumerable<T> Objects) where T : IEntity, new() {
            if (Objects != null & Objects.Count() >= 1) {
                using DataProvider provider = new DataProvider(this.ConnectionObject, GetObject<T>().UpdateProcedureName(), ProviderType.StoredProcedure);
                
                foreach (var obj in Objects) {
                    this.OnUpdating(obj, false);

                    provider.Execute<T>(obj);

                    this.OnUpdated(obj, true);
                }
            }
        }

        protected void Update(IEnumerable<IEntity> Objects) {
            try {
                GetCollections(Objects);

                if (_objects != null & _objects.Count() >= 1) {
                    foreach (var obj in _objects) {
                        this.OnUpdating(obj, false);

                        string storedProcedure = (obj as IEntity).UpdateProcedureName();

                        if (storedProcedure != null) {
                            using DataProvider provider = new DataProvider(this.ConnectionObject, storedProcedure, ProviderType.StoredProcedure);

                            provider.ExecuteObject(obj);

                            this.OnUpdated(obj, true);
                        }
                    }
                }
            } catch (Exception) {
                throw;
            } finally {
                this._objects = null;
            }
        }

        protected virtual void OnInserting(IEntity entity, bool success) {
            Inserting?.Invoke(this, new DataAccessEventArgs() { ModifiedObject = entity, Success = success });
        }

        protected virtual void OnInserted(IEntity entity, bool success) {
            Inserted?.Invoke(this, new DataAccessEventArgs() { ModifiedObject = entity, Success = success });
        }

        protected virtual void OnUpdating(IEntity entity, bool success) {
            Updating?.Invoke(this, new DataAccessEventArgs() { ModifiedObject = entity, Success = success });
        }

        protected virtual void OnUpdated(IEntity entity, bool success) {
            Updated?.Invoke(this, new DataAccessEventArgs() { ModifiedObject = entity, Success = success });
        }

        protected virtual void OnDeleting(IEntity entity, bool success) {
            Deleting?.Invoke(this, new DataAccessEventArgs() { ModifiedObject = entity, Success = success });
        }

        protected virtual void OnDeleted(IEntity entity, bool success) {
            Deleted?.Invoke(this, new DataAccessEventArgs() { ModifiedObject = entity, Success = success });
        }
        #endregion

        #region Private Methdods
        private static IEntity GetObject<T>() where T : IEntity, new() {
            return new T();
        }

        private void GetCollections(IEnumerable<IEntity> Objects) {
            foreach (var obj in Objects) {
                GetCollection(obj);
            }
        }

        private void GetCollection(IEntity obj) {
            if (obj == null) return;
            var type = obj.GetType();
            if (typeof(IEntity).IsAssignableFrom(type)) {
                _objects.Add(obj);
                foreach (var prop in type.GetProperties()) {
                    var get = prop.GetGetMethod();
                    if (!get.IsStatic && get.GetParameters().Length == 0) {
                        if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType)) {
                            var collection = (IEnumerable)get.Invoke(obj, null);
                            if (collection != null) {
                                foreach (var obj1 in collection) {
                                    if (typeof(IEntity).IsAssignableFrom(obj1.GetType())) {
                                        GetCollection(obj1 as IEntity);
                                    }
                                }
                            }
                        } else if (typeof(IEntity).IsAssignableFrom(prop.PropertyType)) {
                            GetCollection(get.Invoke(obj, null) as IEntity);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
