using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Models
{
    public enum Type
    {
        In, Out
    }

    public class WorkerCheck
    {
        public int ID { get; set; }
        public int WorkerId { get; set; }
        public Type Type { get; set; }
        public Worker Worker { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
