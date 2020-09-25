namespace MyLayercake.DataProvider {
    public interface IDatabaseSettings {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string Provider { get; set; }
    }
}
