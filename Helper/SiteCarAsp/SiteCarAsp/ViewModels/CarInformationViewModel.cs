using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    /// <summary>
    /// Модель вьюхи для машин
    /// </summary>
    public class CarInformationViewModel
    {
        public CarInformationViewModel(IEnumerable<CarInformation> carInformation)
        {
            this.Cars = carInformation;
        }

        /// <summary>
        /// Фильтры
        /// </summary>
        public Dictionary<string, IEnumerable<string>> Filters
            => new Dictionary<string, IEnumerable<string>>
            {
                { "type", Cars.Select(x=>x.Type).Distinct() },
                { "body", Cars.Select(x=>x.Body).Distinct() }
            };

        /// <summary>
        /// Машины
        /// </summary>
        public IEnumerable<CarInformation> Cars { get; }

        /// <summary>
        /// Активные фильтры
        /// </summary>
        public FilterViewModel[] ActiveFilters { get; set; }
    }
}
