using System.Collections.Generic;

namespace WorkerPluginAPI.Models.Controllers
{
    /// <summary>
    /// Сбор информации о работнике
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WorkerInfo<T>
    {
        /// <summary>
        /// Работник
        /// </summary>
        public Worker Worker { get; set; }
        /// <summary>
        /// Какие-то характеристики
        /// </summary>
        public List<T> WorkDistances { get; set; } = new List<T>();
    }

    /// <summary>
    /// Характеристика о работе за день
    /// </summary>
    public class DayInfo
    {
        public int Day { get; set; }
        public string Time { get; set; }
    }
}
