using SiteCarAsp.Models;
using SiteCarAsp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCarAsp.Services
{
    /// <summary>
    /// Сервис машин
    /// </summary>
    public interface ICarService
    {
        Task<IEnumerable<CarInformation>> GetCars();
        Task<IEnumerable<CarInformation>> GetUsedCars();
        Task<IEnumerable<CarInformation>> GetNewCars();
        Task<IEnumerable<CarInformation>> GetFilteredCars(FilterViewModel[] filters);
    }

    public class CarsService : ICarService
    {
        private readonly ICarsRepository carsDataProvider;

        public CarsService(ICarsRepository carsDataProvider)
        {
            this.carsDataProvider = carsDataProvider;
        }

        public Task<IEnumerable<CarInformation>> GetCars() => carsDataProvider.GetAllAsync();

        public async Task<IEnumerable<CarInformation>> GetFilteredCars(FilterViewModel[] filters)
        {
            var resultData = await GetCars();
            foreach (var filter in filters)
            {
                resultData = filter switch
                {
                    { Field: "type" } => resultData.Where(x => filter.Val.Contains(x.Type)),
                    { Field: "body" } => resultData.Where(x => filter.Val.Contains(x.Body)),
                    { Field: "used" } => filter.Val.ToLower() == "true" ? resultData.Where(x => x.Used) : resultData.Where(x => !x.Used),
                    _ => resultData
                };
            }
            return resultData;
        }

        public Task<IEnumerable<CarInformation>> GetNewCars() => carsDataProvider.GetNewCarInformationAsync();

        public Task<IEnumerable<CarInformation>> GetUsedCars() => carsDataProvider.GetUsedCarInformationAsync();
    }
}
