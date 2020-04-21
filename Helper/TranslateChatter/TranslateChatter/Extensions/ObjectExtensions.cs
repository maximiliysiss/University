using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateChatter.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsContains(this string str, IEnumerable<string> vs) => vs.Any(x => str.Contains(x));
    }
}
