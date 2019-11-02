using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStation.Models
{
    /// <summary>
    /// Расписание
    /// </summary>
    public class Schedule
    {
        public int ID { get; set; }
        /// <summary>
        /// Время
        /// </summary>
        /// <value></value>
        public string Time { get; set; }
        /// <summary>
        /// Расстояние
        /// </summary>
        /// <value></value>
        public double Distance { get; set; }
        /// <summary>
        /// День недели
        /// </summary>
        /// <value></value>
        public DayOfWeek DayOfWeek { get; set; }
        /// <summary>
        /// От 
        /// </summary>
        /// <value></value>
        public int FromId { get; set; }
        public virtual Point From { get; set; }
        /// <summary>
        /// Куда
        /// </summary>
        /// <value></value>
        public int ToId { get; set; }
        public virtual Point To { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        /// <value></value>
        public double Price { get; set; }
    }
}
