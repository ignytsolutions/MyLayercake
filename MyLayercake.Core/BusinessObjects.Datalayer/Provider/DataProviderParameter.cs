using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace MyLayercake.BusinessObjects.Datalayer.Provider {
    internal class DataProviderParameter {
        internal string Name { get; set; }
        internal DbType DbType { get; set; }
        internal object Val { get; set; }
        internal bool Output { get; set; }
    }
}
