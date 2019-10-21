using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStation.Models
{
    public class Buying
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public string HistorySchedule { get; set; }
        public int Count { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
