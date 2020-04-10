using PeopleAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalysis.ViewModels
{
    public class AnalitycsRequestModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Social { get; set; }
    }

    public class AnalitycsViewModel
    {
        public Status Status { get; set; }
    }
}
