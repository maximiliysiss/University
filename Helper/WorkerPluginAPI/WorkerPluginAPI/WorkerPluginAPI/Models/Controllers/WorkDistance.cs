using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkerPluginAPI.Models.Controllers
{
    public class WorkerInfo
    {
        public Worker Worker { get; set; }
        public List<WorkDistance> WorkDistances { get; set; } = new List<WorkDistance>();
    }

    public class WorkDistance
    {
        [JsonIgnore]
        public DateTime Start { get; set; }
        [JsonIgnore]
        public DateTime End { get; set; }

        public Type StartType { get; set; }
        public Type EndType { get; set; }
        public string StartTypeString => StartType.ToString();
        public string EndTypeString => EndType.ToString();

        public string StartDateTime => Start.ToString("dd.MM.yyyy HH:mm");
        public string EndDateTime => End.ToString("dd.MM.yyyy HH:mm");
    }
}
