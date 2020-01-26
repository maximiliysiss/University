using System.Collections.Generic;

namespace WorkerPluginAPI.Models.Controllers
{
    public class WorkerInfo<T>
    {
        public Worker Worker { get; set; }
        public List<T> WorkDistances { get; set; } = new List<T>();
    }

    public class DayInfo
    {
        public int Day { get; set; }
        public string Time { get; set; }
    }
}
