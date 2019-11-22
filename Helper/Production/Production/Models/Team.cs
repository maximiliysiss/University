using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public class Team
    {
        public int ID { get; set; }
        public int BrigadirId { get; set; }
        public User Brigadir { get; set; }
    }
}
