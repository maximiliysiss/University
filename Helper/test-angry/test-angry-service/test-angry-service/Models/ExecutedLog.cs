using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_angry_service.Models
{
    public class ExecutedLog
    {
        public int Id { get; set; }
        /// <summary>
        /// IP address
        /// </summary>
        public string Name { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Result
        /// </summary>
        public float AngryPercent { get; set; }
    }
}
