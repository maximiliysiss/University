﻿using Production.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    /// <summary>
    /// Отчет
    /// </summary>
    public class Report
    {
        [HideColumnIfAutoGenerated]
        public int ID { get; set; }
        [HideColumnIfAutoGenerated]
        public int DayPlanId { get; set; }
        [DisplayGridName("План на день")]
        public DayPlan DayPlan { get; set; }
        [DisplayGridName("Сделано деталей")]
        public int CompleteDetail { get; set; }
        [DisplayGridName("Количество брака")]
        public int FailDetail { get; set; }
        [HideColumnIfAutoGenerated]
        public int UserId { get; set; }
        [DisplayGridName("Автор")]
        public User User { get; set; }
    }
}
