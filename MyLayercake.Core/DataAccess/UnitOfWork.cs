using System;
using System.Data;

namespace MyLayercake.Core.DataAccess {
    public class UnitOfWork : IUnitOfWork {
        public IDatabaseContext Context { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IDatabaseContext context) {
            Context = context ?? throw new ArgumentException("Database Context has not been initialized");
        }

        public void Commit() {
            if (Transaction != null) {
                try {
                    Transaction.Commit();
                } catch (Exception) {
                    Transaction.Rollback();
                }
                Transaction.Dispose();
                Transaction = null;
            } else {
                throw new NullReferenceException("Tryed commit not opened transaction");
            }
        }

        /// <summary>
        /// Define a property of context class
        /// </summary>
        public IDatabaseContext DataContext {
            get { return Context; }
        }

        /// <summary>
        /// Begin a database transaction
        /// </summary>
        /// <returns>Transaction</returns>
        public IDbTransaction BeginTransaction() {
            if (Transaction != null) {
                throw new NullReferenceException("Not finished previous transaction");
            }

            Transaction = Context.Connection.BeginTransaction();

            return Transaction;
        }

        public void Dispose() {
            if (Transaction != null) {
                Transaction.Dispose();
            }

            if (Context != null) {
                Context.Dispose();
            }
        }
    }
}
