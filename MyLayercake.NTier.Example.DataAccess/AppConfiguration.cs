using System.Configuration;

namespace MyLayercake.NTier.Example.DataAccess {
    /// <summary>
    /// The AppConfiguaration class contains read-only properties that are essentially short cuts to settings in the web.config file.
    /// </summary>
    public static class AppConfiguration {

        #region Public Properties

        /// <summary>
        /// Returns the connectionstring  for the application.
        /// </summary>
        public static string ConnectionString {
            get {
                return ConfigurationManager.ConnectionStrings["NLayer"].ConnectionString;
            }
        }
        #endregion

    }
}