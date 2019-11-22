using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public class Schedule
    {
        public int ID { get; set; }
        public int DetailId { get; set; }
        public Detail Detail { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
    }
}
