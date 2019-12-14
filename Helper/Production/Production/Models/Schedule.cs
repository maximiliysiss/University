﻿using Production.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public class Schedule
    {
        [HideColumnIfAutoGenerated]
        public int ID { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int Queue { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        public override string ToString() => $"{Team.Name} #{Queue} / {Date.ToString("dd.MM.yyyy")}";
    }
}
