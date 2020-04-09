using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalysis.Extensions
{
    public static class ObjectExtensions
    {
        public static bool In<T>(this T obj, IEnumerable<T> list) => list.Contains(obj);
    }
}
