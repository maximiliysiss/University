using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalysis.Models
{
    public enum Status
    {
        Create,
        InProgress,
        Fail,
        Complete
    }

    public class Request
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string User { get; set; }
        public string UserId { get; set; }
        public string Social { get; set; }
        public int OwnerId { get; set; }
        public int CreateId { get; set; }
        public Status Status { get; set; }
    }
}
