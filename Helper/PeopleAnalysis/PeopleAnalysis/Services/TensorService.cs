using PeopleAnalysis.Models;
using System.Threading.Tasks;

namespace PeopleAnalysis.Services
{
    public interface IAIService
    {
        Task ProcessTaskAsync(Request request, IAnaliticAIService analiticService, DatabaseContext databaseContext);
    }

    public class TensorService : IAIService
    {

        public async Task ProcessTaskAsync(Request request, IAnaliticAIService analiticService, DatabaseContext databaseContext)
        {
            //TODO
            await analiticService.InProcessAsync(request, 0, databaseContext);
        }
    }
}
