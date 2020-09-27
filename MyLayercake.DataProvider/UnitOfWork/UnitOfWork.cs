using MyLayercake.DataProvider.Factory;
using System.Transactions;

namespace MyLayercake.DataProvider.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {
        public IDBContext Context { get; }

        private readonly TransactionScope _transactionScope;

        public UnitOfWork(IDBContext context) {
            Context = context;
            _transactionScope = new TransactionScope();
        }
        public void Commit() {
            _transactionScope.Complete();
        }

        public void Dispose() {
            Context.Dispose();
        }
    }
}
