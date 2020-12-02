using SiteCarAsp.Models;

namespace SiteCarAsp.Services
{
    public interface ICreditRepository : IDataProvider<Credit>
    {
    }

    public class CreditRepository : FileDataProvider<Credit>, ICreditRepository
    {
        public CreditRepository(string filePath) : base(filePath)
        {
        }
    }
}
