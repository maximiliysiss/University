using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Extensions
{
    public class StringUtils
    {
        internal static bool IsNullOrEmpty(params string[] strs) => strs.All(x => string.IsNullOrEmpty(x));
    }
}
