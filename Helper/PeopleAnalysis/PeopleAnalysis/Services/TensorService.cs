using PeopleAnalysis.Models;
using System.Collections.Generic;
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

            var readyResult = new Result
            {
                ResultAnswer = true,
            };

            var analysObjects = new[] {
                new AnalysObject{ Name = "Boat", Weight = 1 },
                new AnalysObject{ Name = "City", Weight = 1 },
                new AnalysObject{ Name = "House", Weight = 1 },
                new AnalysObject{ Name = "Square", Weight = 1 }
            };

            readyResult.ResultObjects = new List<ResultObject>();
            readyResult.ResultObjects.Add(new ResultObject { Count = 1, Result = readyResult, AnalysObject = analysObjects[0] });
            readyResult.ResultObjects.Add(new ResultObject { Count = 2, Result = readyResult, AnalysObject = analysObjects[1] });
            readyResult.ResultObjects.Add(new ResultObject { Count = 1, Result = readyResult, AnalysObject = analysObjects[2] });
            readyResult.ResultObjects.Add(new ResultObject { Count = 3, Result = readyResult, AnalysObject = analysObjects[3] });

            await analiticService.ReadyResult(request, request.CreateId, databaseContext, readyResult);
        }
    }
}
