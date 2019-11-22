using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Models
{
    public class FailDetail
    {
        public int ID { get; set; }
        public int Count { get; set; }
        public string DetailName { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Today;
    }
}
