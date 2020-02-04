using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool InMonth(this DateTime dateTime, int year, int month) => dateTime.Year == year && dateTime.Month == month;
    }
}
