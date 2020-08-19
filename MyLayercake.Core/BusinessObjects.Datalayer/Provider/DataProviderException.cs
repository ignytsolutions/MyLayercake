using System;

namespace MyLayercake.BusinessObjects.Datalayer.Provider {
    public class DataProviderException : Exception {
        public DataProviderException() { }

        public DataProviderException(string message)
            : base(string.Format("DataProvider error: {0}", message)) {
        }
    }
}