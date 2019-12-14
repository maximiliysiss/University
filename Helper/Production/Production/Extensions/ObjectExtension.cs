using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Extensions
{
    public static class ObjectExtension
    {
        public static List<T> ToList<T>(this T obj) => new List<T> { obj };
    }
}
