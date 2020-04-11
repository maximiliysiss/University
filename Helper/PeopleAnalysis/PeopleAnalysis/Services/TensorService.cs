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
            await analiticService.InProcessAsync(request, request.CreateId, databaseContext);

            // TODO magic

            await analiticService.ReadyResult(request, request.CreateId, databaseContext);
        }
    }
}
