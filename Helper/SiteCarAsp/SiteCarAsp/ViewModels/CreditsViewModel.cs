using Microsoft.AspNetCore.Mvc.Rendering;
using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    public class CreditsViewModel
    {
        public IEnumerable<CarInformation> CarInformations { get; set; }
        public int CarId { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }

        public SelectList Cars => new SelectList(CarInformations ?? Enumerable.Empty<CarInformation>(), "Id", "Name");

    }
}
