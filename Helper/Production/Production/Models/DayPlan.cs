﻿using Production.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    /// <summary>
    /// План дня
    /// </summary>
    public class DayPlan
    {
        [HideColumnIfAutoGenerated]
        public int ID { get; set; }
        [HideColumnIfAutoGenerated]
        public int DetailId { get; set; }
        [DisplayGridName("Деталь")]
        public Detail Detail { get; set; }
        [DisplayGridName("Количество")]
        public int Count { get; set; }
        [HideColumnIfAutoGenerated]
        public int ScheduleId { get; set; }
        [DisplayGridName("План")]
        public Schedule Schedule { get; set; }

        public override string ToString() => $"{Detail.Name} / {Schedule}";
    }
}
