using System.Collections.Generic;

namespace MyLayercake.Core {
    public interface IRulesContainer {
        List<IRule[]> Rules { get; }
    }
}
