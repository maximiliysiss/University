using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SchoolService.Models
{
    public class Child : User
    {
        public virtual Class Class { get; set; }
        [JsonIgnoreAttribute]
        public virtual List<Mark> Marks { get; set; }
        [JsonIgnoreAttribute]
        public virtual List<ChildInRiskGroup> ChildInRiskGroups { get; set; }
        public bool IsArchive { get; set; }
    }
}