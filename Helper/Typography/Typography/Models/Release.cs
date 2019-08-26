using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Models
{
    public class Release
    {
        [Key]
        public int ReleaseID { get; set; }
        public virtual Typography Typography { get; set; }
        public virtual Paper Paper { get; set; }
    }
}
