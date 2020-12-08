using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    public class CarInformationViewModel
    {
        public CarInformationViewModel(IEnumerable<CarInformation> carInformation)
        {
            this.Cars = carInformation;
        }

        public Dictionary<string, IEnumerable<string>> Filters
            => new Dictionary<string, IEnumerable<string>>
            {
                { "type", Cars.Select(x=>x.Type).Distinct() },
                { "body", Cars.Select(x=>x.Body).Distinct() }
            };

        public IEnumerable<CarInformation> Cars { get; }

        public FilterViewModel[] ActiveFilters { get; set; }
    }
}
