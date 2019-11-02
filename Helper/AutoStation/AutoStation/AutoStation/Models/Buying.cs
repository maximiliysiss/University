using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStation.Models
{
    /// <summary>
    /// Покупки
    /// </summary>
    public class Buying
    {
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Поездка
        /// </summary>
        /// <value></value>
        public int? ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        /// <summary>
        /// Сохраним, если вдруг удалят путь
        /// </summary>
        /// <value></value>
        public string HistorySchedule { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        /// <value></value>
        public int Count { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        /// <value></value>
        public double Sum { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public string GuidNumber { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Дата и время
        /// </summary>
        /// <value></value>
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
