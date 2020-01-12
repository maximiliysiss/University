using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClearPluginAPI.Models
{
    public class ClearAction
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string Type { get; set; }
    }
}
