using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteCarAsp.Services.Controller
{
    /// <summary>
    /// Сервис кредитов
    /// </summary>
    public interface ICreditsService
    {
        Task<IEnumerable<CarInformation>> GetCarsAsync();
        Task AddAsync(Credit credit);
    }

    public class CreditsService : ICreditsService
    {
        private readonly ICarsRepository carsDataProvider;
        private readonly IDataProvider<Credit> creditRepository;

        public CreditsService(ICarsRepository carsDataProvider, IDataProvider<Credit> creditRepository)
        {
            this.carsDataProvider = carsDataProvider;
            this.creditRepository = creditRepository;
        }

        public Task AddAsync(Credit credit) => creditRepository.AddAsync(credit);

        public Task<IEnumerable<CarInformation>> GetCarsAsync() => carsDataProvider.GetAllAsync();
    }
}
