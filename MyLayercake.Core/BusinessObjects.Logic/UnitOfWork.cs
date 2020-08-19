using MyLayercake.BusinessObjects.Datalayer;
using MyLayercake.BusinessObjects.Datalayer.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace MyLayercake.BusinessObjects.Logic {
    public abstract class UnitOfWork<T> : DataAccess, IDisposable where T : IEntity, new() {
        public event EventHandler Commiting;
        public event EventHandler Commited;

        #region Privates
        private List<T> _changed = new List<T>();
        private List<T> _new = new List<T>();
        private List<T> _deleted = new List<T>();
        #endregion

        #region Constructor
        protected UnitOfWork(Connection ConnectionObject) : base(ConnectionObject) { }
        protected UnitOfWork() : base() { }
        #endregion

        #region Methods
        protected void AddInsertObjects(IEnumerable<T> Objects) {
            foreach (var obj in Objects) {
                _new.Add(obj);
            }
        }

        protected void AddUpdateObjects(IEnumerable<T> Objects) {
            foreach (var obj in Objects) {
                _changed.Add(obj);
            }
        }

        protected void AddDeleteObjects(IEnumerable<T> Objects) {
            foreach (var obj in Objects) {
                _deleted.Add(obj);
            }
        }

        protected IEnumerable<T> SelectObjects(IEnumerable<T> Objects) {
            return this.Get<T>(Objects);
        }

        protected IEnumerable<T> SelectObjects() {
            return this.Get<T>();
        }

        protected IEnumerable<T> SelectObjects(int ID) {
            return this.Get<T>(ID);
        }

        protected void CommitUnitOfWork() {
            this.OnCommiting();

            using TransactionScope scope = new TransactionScope();

            if (_changed != null & _changed.Count() >= 1) {
                this.Update<T>(_changed);
            }

            if (_new != null & _new.Count() >= 1) {
                this.Insert<T>(_new);
            }

            if (_deleted != null & _deleted.Count() >= 1) {
                this.Delete<T>(_deleted);
            }

            scope.Complete();

            this.OnCommited();

            _changed = null;
            _new = null;
            _deleted = null;
        }
        #endregion

        protected virtual void OnCommiting() {
            Commiting?.Invoke(this, new EventArgs());
        }

        protected virtual void OnCommited() {
            Commited?.Invoke(this, new EventArgs());
        }

        #region Dispose
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (_changed != null) {
                    _changed = null;
                }
                if (_new != null) {
                    _new = null;
                }
                if (_deleted != null) {
                    _deleted = null;
                }
            }
        }

        ~UnitOfWork() {
            Dispose(false);
        }
        #endregion
    }
}
