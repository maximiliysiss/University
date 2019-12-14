using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public class DayPlan
    {
        public int ID { get; set; }
        public int DetailId { get; set; }
        public Detail Detail { get; set; }
        public int Count { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }

        public override string ToString() => $"{Detail.Name} / {Schedule}";
    }
}
