using MyLayercake.BusinessObjects.Datalayer.Provider;
using System;
using System.Collections.Generic;

namespace MyLayercake.BusinessObjects.Logic {
    public abstract class RepositoryBase<T> : UnitOfWork<T>, IDisposable where T : IEntity, new() {
        protected RepositoryBase(Connection ConnectionObject) : base(ConnectionObject) { }
        protected RepositoryBase() : base() { }

        public void Delete(IEnumerable<T> Objects) {
            this.AddDeleteObjects(Objects);
        }

        public void Delete(T Object) {
            this.AddDeleteObjects(new List<T>(new T[] { Object }));
        }

        public void Insert(IEnumerable<T> Objects) {
            this.AddInsertObjects(Objects);
        }

        public void Insert(T Object) {
            this.AddInsertObjects(new List<T>(new T[] { Object }));
        }

        public IEnumerable<T> Select() {
            return this.SelectObjects();
        }

        public IEnumerable<T> Select(IEnumerable<T> Objects) {
            return this.SelectObjects(Objects);
        }

        public IEnumerable<T> Select(T Object) {
            return this.SelectObjects(new List<T>(new T[] { Object }));
        }

        public IEnumerable<T> Select(int ID) {
            return this.SelectObjects(ID);
        }

        public void Update(IEnumerable<T> Objects) {
            this.AddUpdateObjects(Objects);
        }

        public void Update(T Object) {
            this.AddUpdateObjects(new List<T>(new T[] { Object }));
        }

        public void Commit() {
            this.CommitUnitOfWork();
        }
    }
}
