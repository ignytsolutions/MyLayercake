using MyLayercake.Core.DataAccess;
using System.Data;

namespace MyLayercake.Core.Extentions {
    internal static class IDatabaseContextExtentions {
        public static IDbCommand CreateCommand(this IDatabaseContext context) {
            return context.Connection.CreateCommand();
        }
    }
}