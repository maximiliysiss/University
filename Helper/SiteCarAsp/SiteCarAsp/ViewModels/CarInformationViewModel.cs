using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    public class Filter
    {
        public string Caption { get; set; }
        public string Field { get; set; }
    }

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
        public Dictionary<Filter, IEnumerable<string>> Filters
            => new Dictionary<Filter, IEnumerable<string>>
            {
                { new Filter{ Caption = "Тип", Field = "type" }, Cars.Select(x=>x.Type).Distinct() },
                { new Filter{ Caption = "Кузов", Field = "body" }, Cars.Select(x=>x.Body).Distinct() }
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
