using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Models
{
    public class WorkerCheck
    {
        public int ID { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
