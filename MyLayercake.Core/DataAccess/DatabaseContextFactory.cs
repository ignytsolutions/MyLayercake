namespace MyLayercake.Core.DataAccess {
    public class DatabaseContextFactory : IDatabaseContextFactory {
        public DatabaseContextFactory(IConnectionProvider connectionProvider) {
            Context ??= new DatabaseContext(connectionProvider);
        }

        public IDatabaseContext Context { get; }

        public void Dispose() {
            if (Context != null)
                Context.Dispose();
        }
    }
}
