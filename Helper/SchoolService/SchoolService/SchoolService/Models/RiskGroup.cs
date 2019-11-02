using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models
{
    public class RiskGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<ChildInRiskGroup> ChildInRiskGroups { get; set; }
    }

    public class ChildInRiskGroup
    {
        public int ID { get; set; }
        public int RiskGroupId { get; set; }
        [JsonIgnoreAttribute]
        public virtual RiskGroup RiskGroup { get; set; }
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }
    }
}
