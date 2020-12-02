using SiteCarAsp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteCarAsp.Services
{
    public interface ITestDriveService
    {
        Task<IEnumerable<CarInformation>> GetCarsAsync();
        Task AddTestDriveAsync(TestDrive testDrive);
    }

    public class TestDriveService : ITestDriveService
    {
        private readonly ICarsRepository carsDataProvider;
        private readonly ITestDriveRepository testDriveDataProvider;

        public TestDriveService(ICarsRepository carsDataProvider, ITestDriveRepository testDriveDataProvider)
        {
            this.carsDataProvider = carsDataProvider;
            this.testDriveDataProvider = testDriveDataProvider;
        }

        public Task AddTestDriveAsync(TestDrive testDrive) => testDriveDataProvider.AddAsync(testDrive);

        public Task<IEnumerable<CarInformation>> GetCarsAsync() => carsDataProvider.GetAllAsync();
    }
}
