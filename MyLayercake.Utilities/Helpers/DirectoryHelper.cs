using System;
using System.IO;

namespace MyLayercake.Utilities.Helpers {
    public static class DirectoryHelper {
        public static string GetDataDirectory() {
            return Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Data");
        }
    }
}
