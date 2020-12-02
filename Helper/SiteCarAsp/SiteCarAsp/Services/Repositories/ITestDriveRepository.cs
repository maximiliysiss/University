using SiteCarAsp.Models;

namespace SiteCarAsp.Services
{
    public interface ITestDriveRepository : IDataProvider<TestDrive>
    {
    }

    public class TestDriveRepository : FileDataProvider<TestDrive>, ITestDriveRepository
    {
        public TestDriveRepository(string filePath) : base(filePath)
        {
        }
    }
}
