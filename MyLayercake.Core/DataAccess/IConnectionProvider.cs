namespace MyLayercake.Core.DataAccess {
    public interface IConnectionProvider {
        string Provider { get; set; }
        string ConnectionString { get; set; }
        bool OpenConnection { get; set; }
    }
}
