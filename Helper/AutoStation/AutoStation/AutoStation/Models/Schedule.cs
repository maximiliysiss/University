using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStation.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public string Time { get; set; }
        public double Distance { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int FromId { get; set; }
        public virtual Point From { get; set; }
        public int ToId { get; set; }
        public virtual Point To { get; set; }
        public double Price { get; set; }
    }
}
