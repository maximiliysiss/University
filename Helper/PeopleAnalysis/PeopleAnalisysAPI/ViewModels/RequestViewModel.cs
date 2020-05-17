using PeopleAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalisysAPI.ViewModels
{
    public class RequestViewModel
    {
        public Status Status { get; internal set; }
        public string OwnerId { get; internal set; }
        public string Social { get; internal set; }
        public string User { get; internal set; }
        public string UserId { get; internal set; }
        public Uri UserUrl { get; internal set; }
        public int Id { get; internal set; }
        public string CreateId { get; internal set; }
        public DateTime DateTime { get; internal set; }
    }
}
