using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteCarAsp.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarInformation>> GetCars();
        Task<IEnumerable<CarInformation>> GetUsedCars();
        Task<IEnumerable<CarInformation>> GetNewCars();
        Task<IEnumerable<CarInformation>> GetFilteredCars(Dictionary<string, string> filters);
    }

    public class CarsService : ICarService
    {
        private readonly ICarsRepository carsDataProvider;

        public CarsService(ICarsRepository carsDataProvider)
        {
            this.carsDataProvider = carsDataProvider;
        }

        public Task<IEnumerable<CarInformation>> GetCars() => carsDataProvider.GetAllAsync();

        public async Task<IEnumerable<CarInformation>> GetFilteredCars(Dictionary<string, string> filters)
        {
            var resultData = await GetCars();
            foreach (var filter in filters)
            {
                resultData = filter switch
                {
                    { Key: "type" } => resultData.Where(x => filter.Value.Contains(x.Type)),
                    { Key: "body" } => resultData.Where(x => filter.Value.Contains(x.Body)),
                    { Key: "used" } => filter.Value == "true" ? resultData.Where(x => x.Used) : resultData.Where(x => !x.Used),
                    _ => resultData
                };
            }
            return resultData;
        }

        public Task<IEnumerable<CarInformation>> GetNewCars() => carsDataProvider.GetNewCarInformationAsync();

        public Task<IEnumerable<CarInformation>> GetUsedCars() => carsDataProvider.GetUsedCarInformationAsync();
    }
}
