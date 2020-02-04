using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Extensions
{
    public static class StringUtils
    {
        /// <summary>
        /// Пусты ли строки
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(params string[] strs) => strs.All(x => string.IsNullOrEmpty(x));

        /// <summary>
        /// Перевести тики в формат HH:mm:ss
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static string ToTimeString(this long ticks)
        {
            var date = new TimeSpan(ticks);
            return $"{date.TotalHours.Addition()}:{date.Minutes.Addition()}:{date.Seconds.Addition()}";
        }

        /// <summary>
        /// Расчет добавления нуля
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static string Addition(this int time) => $"{(time < 10 ? "0" : "")}{time}";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static string Addition(this double time) => ((int)time).Addition();
    }
}
