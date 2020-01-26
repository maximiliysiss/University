using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Extensions
{
    public static class StringUtils
    {
        public static bool IsNullOrEmpty(params string[] strs) => strs.All(x => string.IsNullOrEmpty(x));

        public static string ToTimeString(this long ticks)
        {
            var date = new TimeSpan(ticks);
            return $"{date.TotalHours.Addition()}:{date.Minutes.Addition()}:{date.Seconds.Addition()}";
        }

        private static string Addition(this int time) => $"{(time < 10 ? "0" : "")}{time}";
        private static string Addition(this double time) => ((int)time).Addition();
    }
}
