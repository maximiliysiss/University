using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteCarAsp.Services.Controller
{
    public interface ICreditsService
    {
        Task<IEnumerable<CarInformation>> GetCarsAsync();
        Task AddAsync(Credit credit);
    }

    public class CreditsService : ICreditsService
    {
        private readonly ICarsRepository carsDataProvider;
        private readonly ICreditRepository creditRepository;

        public CreditsService(ICarsRepository carsDataProvider, ICreditRepository creditRepository)
        {
            this.carsDataProvider = carsDataProvider;
            this.creditRepository = creditRepository;
        }

        public Task AddAsync(Credit credit) => creditRepository.AddAsync(credit);

        public Task<IEnumerable<CarInformation>> GetCarsAsync() => carsDataProvider.GetAllAsync();
    }
}
