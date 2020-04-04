using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNSmart.Connector
{
    public static class ReferencedAssemblies
    {
        public static IEnumerable<string> Assemblies { get; } = new string[]
        {
            //nameof(QuickNSmart.Contracts),
            //nameof(QuickNSmart.Adapters),
        };
    }
}
