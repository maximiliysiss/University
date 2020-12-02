using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    public class CarInformationViewModel
    {
        private readonly IEnumerable<CarInformation> carInformation;

        public CarInformationViewModel(IEnumerable<CarInformation> carInformation)
        {
            this.carInformation = carInformation;
        }

        public Dictionary<string, IEnumerable<string>> Filters
            => new Dictionary<string, IEnumerable<string>>
            {
                { "type", carInformation.Select(x=>x.Type).Distinct() },
                { "body", carInformation.Select(x=>x.Body).Distinct() }
            };

        public IEnumerable<CarInformation> Cars => carInformation;

        public FilterViewModel[] ActiveFilters { get; set; }
    }
}
