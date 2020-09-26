using MyLayercake.Core;

namespace MyLayercake.DataProvider.Factory {
    // Factory Design Pattern
    public abstract class ProviderFactory {
        public abstract DataProvider GetDataProvider();
    }
}
