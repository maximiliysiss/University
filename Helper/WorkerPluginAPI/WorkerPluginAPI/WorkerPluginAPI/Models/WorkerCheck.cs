using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Models
{
    /// <summary>
    /// Состояния
    /// </summary>
    public enum Type
    {
        In, Out, Pause, Continue, Custom
    }

    /// <summary>
    /// Отметка о состоянии работы
    /// </summary>
    public class WorkerCheck
    {
        public int ID { get; set; }
        public int WorkerId { get; set; }
        public Type Type { get; set; }
        public Worker Worker { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
