using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using PeopleAnalysis.Models;
using PeopleAnalysisML.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PeopleAnalysis.Services
{
    public interface IAIService
    {
        Task ProcessTaskAsync(Request request, IAnaliticAIService analiticService, DatabaseContext databaseContext, ApisManager apisManager);
    }

    public class TensorService : IAIService
    {
        private readonly IMLService mLService;
        private readonly ILogger<TensorService> logger;

        public TensorService(IMLService mLService, ILogger<TensorService> logger)
        {
            this.mLService = mLService;
            this.logger = logger;
        }

        public async Task ProcessTaskAsync(Request request, IAnaliticAIService analiticService, DatabaseContext databaseContext, ApisManager apisManager)
        {
            logger.LogInformation("Start processing");
            await analiticService.InProcessAsync(request, request.CreateId, databaseContext);

            var api = apisManager[request.Social];
            var detail = api.GetUserDetailInformationView(request.UserId);

            List<string> files = new List<string>();
            var path = Path.Combine(System.IO.Path.GetTempPath(), $"{request.UserId}_{Environment.TickCount64}");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            logger.LogInformation("Download files");
            using (var client = new WebClient())
            {
                foreach (var photo in detail.Photos)
                {
                    var str = Path.Combine(path, photo.Segments.Last());
                    client.DownloadFile(photo, str);
                    files.Add(str);
                }
            }

            logger.LogInformation("Analysis files");
            Dictionary<string, float> resultSet = new Dictionary<string, float>();
            foreach (var photo in files)
            {
                foreach (var val in mLService.Predict(new ModelInput { ImageSource = photo }).Where(x => x.Value > 0.10))
                {
                    if (resultSet.TryGetValue(val.Key, out var tmp))
                        resultSet[val.Key] = (resultSet[val.Key] + tmp) / 2;
                    else
                        resultSet[val.Key] = val.Value;
                }
            }

            logger.LogInformation("Create result");
            var readyResult = new Result
            {
                ResultAnswer = true,
                ResultObjects = new List<ResultObject>(),
                Request = request
            };

            foreach (var obj in resultSet)
            {
                readyResult.ResultObjects.Add(new ResultObject
                {
                    Count = obj.Value,
                    Result = readyResult,
                    AnalysObject = new AnalysObject
                    {
                        Name = obj.Key,
                        Weight = 1
                    }
                });
            }

            await analiticService.ReadyResult(request, request.CreateId, databaseContext, readyResult);
        }
    }
}
