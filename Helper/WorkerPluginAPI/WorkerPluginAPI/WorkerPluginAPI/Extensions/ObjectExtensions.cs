using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Extensions
{
    public static class ObjectExtensions
    {
        public static bool In<T>(this T obj, params T[] values) => values.Any(x => x.Equals(obj));
        public static bool NotIn<T>(this T obj, params T[] values) => !obj.In(values);
    }
}
