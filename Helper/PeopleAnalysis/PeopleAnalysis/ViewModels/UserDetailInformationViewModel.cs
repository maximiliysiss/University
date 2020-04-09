using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalysis.ViewModels
{
    public class UserDetailInformationViewModel
    {
        public string FullName { get; set; }
        public string Id { get; set; }
        public string Birthday { get; set; }
        public Uri Photo { get; set; }
        public Uri[] Photos { get; set; }
        public Uri PageUrl { get; set; }
        public bool IsPrivate { get; set; }
        public string Social { get; set; }

        public AnalitycsViewModel AnalitycsViewModel { get; set; }
    }
}
