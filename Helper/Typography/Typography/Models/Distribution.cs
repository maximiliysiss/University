using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Models
{
    public class Distribution
    {
        [Key]
        public int DistributionID { get; set; }
        public virtual Paper Paper { get; set; }
        public virtual PostOfficer PostOfficer { get; set; }
    }
}
