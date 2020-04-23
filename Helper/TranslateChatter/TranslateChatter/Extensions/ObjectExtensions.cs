using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateChatter.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Содержит ли строка какую-либо строку из списка
        /// </summary>
        /// <param name="str"></param>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static bool IsContains(this string str, IEnumerable<string> vs) => vs.Any(x => str.Contains(x));
    }
}
