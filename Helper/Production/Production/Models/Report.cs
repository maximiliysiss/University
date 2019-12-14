using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public class Report
    {
        public int ID { get; set; }
        public int DayPlanId { get; set; }
        public DayPlan DayPlan { get; set; }
        public int CompleteDetail { get; set; }
        public int FailDetail { get; set; }
    }
}
