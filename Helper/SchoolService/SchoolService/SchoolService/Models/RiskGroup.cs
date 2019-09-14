using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models
{
    public class RiskGroup
    {
        public int ID { get; set; }
        public virtual Child Child { get; set; }
        public string Description { get; set; }
    }
}
