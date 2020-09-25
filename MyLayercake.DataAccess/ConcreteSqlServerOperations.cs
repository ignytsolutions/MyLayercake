using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace MyLayercake.DataAccess {
    /// <summary>
    /// Performs the Database operations implementing the Template Method Design Pattern
    /// </summary>
    /// <implements>
    /// Implements abstract Interface IDBOperations
    /// </implements>
    public class ConcreteSqlServerOperations : DatabaseOperations {
		public ConcreteSqlServerOperations(DbProviderFactory dbProviderFactory) : base(dbProviderFactory) { }

        public override void Delete<T>(T objectToUpdate) {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync<T>(T objectToUpdate) {
            throw new NotImplementedException();
        }

        public override void Insert<T>(T objectToInsert) {
            throw new NotImplementedException();
        }

        public override Task InsertAsync<T>(T objectToInsert) {
            throw new NotImplementedException();
        }

        public override void Select<T>() {
            throw new NotImplementedException();
        }

        public override void SelectByID<T>(object ID) {
            throw new NotImplementedException();
        }

        public override Task SelectByIDAsync<T>(object ID) {
            throw new NotImplementedException();
        }

        public override void Update<T>(T objectToUpdate) {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync<T>(T objectToUpdate) {
            throw new NotImplementedException();
        }
    }
}
