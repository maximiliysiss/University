using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteCarAsp.Services
{
    /// <summary>
    /// Сервис тестдрайва
    /// </summary>
    public interface ITestDriveService
    {
        Task<IEnumerable<CarInformation>> GetCarsAsync();
        Task AddTestDriveAsync(TestDrive testDrive);
    }

    public class TestDriveService : ITestDriveService
    {
        private readonly ICarsRepository carsDataProvider;
        private readonly IDataProvider<TestDrive> testDriveDataProvider;

        public TestDriveService(ICarsRepository carsDataProvider, IDataProvider<TestDrive> testDriveDataProvider)
        {
            this.carsDataProvider = carsDataProvider;
            this.testDriveDataProvider = testDriveDataProvider;
        }

        public Task AddTestDriveAsync(TestDrive testDrive) => testDriveDataProvider.AddAsync(testDrive);

        public Task<IEnumerable<CarInformation>> GetCarsAsync() => carsDataProvider.GetAllAsync();
    }
}
