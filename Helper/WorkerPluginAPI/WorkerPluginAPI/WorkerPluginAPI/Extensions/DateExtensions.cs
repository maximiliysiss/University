using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Extensions
{
    public static class DateExtensions
    {
        public static bool InMonth(this DateTime dateTime, DateTime date) => dateTime.Year == date.Year && dateTime.Month == date.Month;
        public static bool InMonth(this DateTime dateTime, int year, int month) => dateTime.Year == year && dateTime.Month == month;
    }
}
