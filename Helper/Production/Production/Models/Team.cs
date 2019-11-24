﻿using Production.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Production.Models
{
    public class Team
    {
        [HideColumnIfAutoGenerated]
        public int ID { get; set; }
        [DisplayGridName("Название")]
        public string Name { get; set; }
        [HideColumnIfAutoGenerated]
        public int BrigadirId { get; set; }
        [DisplayGridName("Бригадир")]
        public User Brigadir { get; set; }
        [HideColumnIfAutoGenerated]
        public List<UserInTeam> Users { get; set; }

        public override string ToString() => Name;
    }

    public class UserInTeam
    {
        [HideColumnIfAutoGenerated]
        public int ID { get; set; }
        [HideColumnIfAutoGenerated]
        public int WorkerId { get; set; }
        [DisplayGridName("Работник")]
        public User Worker { get; set; }
        [HideColumnIfAutoGenerated]
        public int TeamId { get; set; }
        [DisplayGridName("Бригада")]
        public Team Team { get; set; }
    }
}
