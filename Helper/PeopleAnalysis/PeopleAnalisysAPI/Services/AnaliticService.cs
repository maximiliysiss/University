using PeopleAnalysis.Models;
using PeopleAnalysis.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalysis.Services
{
    public class AnaliticService
    {
        private readonly IDatabaseContext databaseContext;
        private readonly ApisManager apisManager;
        private readonly ISender sender;

        public AnaliticService(IDatabaseContext databaseContext, ApisManager apisManager, ISender sender)
        {
            this.databaseContext = databaseContext;
            this.apisManager = apisManager;
            this.sender = sender;
        }

        public AnalitycsViewModel GetAnaliticsAboutUser(string userId, string social, string currentUserId)
        {
            var lastAnalitics = databaseContext.Requests.OrderByDescending(x => x.Id)
                .FirstOrDefault(x => x.UserId == userId && x.Social == social && x.OwnerId == currentUserId);
            if (lastAnalitics == null)
                return null;
            Result result = null;
            if (lastAnalitics.Status == Status.Complete)
                result = databaseContext.Results.FirstOrDefault(x => x.Request.Id == lastAnalitics.Id);
            return new AnalitycsViewModel
            {
                Status = lastAnalitics.Status,
                Result = result,
                Time = lastAnalitics.TimeComplete
            };
        }

        public bool CreateRequest(AnalitycsRequestModel analitycsRequest, string user)
        {
            var isExists = databaseContext.Requests.Any(x => x.OwnerId == user && x.Social == analitycsRequest.Social && x.User == analitycsRequest.Id && x.Status == Status.Complete);
            if (isExists)
                return false;
            var newRequest = new Models.Request
            {
                CreateId = user,
                OwnerId = user,
                Status = Status.Create,
                User = analitycsRequest.UserName,
                Social = analitycsRequest.Social,
                UserId = analitycsRequest.Id,
                UserUrl = apisManager[analitycsRequest.Social].GetUserUri(analitycsRequest.Id)
            };
            databaseContext.Add(newRequest);
            databaseContext.SaveChanges();
            sender.Send(newRequest);
            return true;
        }
    }

    public interface IAnaliticAIService
    {
        Task<Request> InProcessAsync(Request request, string user, DatabaseContext databaseContext);
        Task ReadyResult(Request request, string createId, DatabaseContext databaseContext, Result readyResult);
    }

    public class AnaliticAIService : IAnaliticAIService
    {
        public async Task<Request> InProcessAsync(Request request, string user, DatabaseContext databaseContext)
        {
            var find = databaseContext.Requests.Find(request.Id);
            if (find == null)
                throw new ApplicationException("Not found request");
            if (find.Status != request.Status)
                throw new ApplicationException("Request is changed");

            var newRequest = new Request
            {
                CreateId = user,
                OwnerId = request.OwnerId,
                Social = request.Social,
                Status = Status.InProgress,
                User = request.User,
                UserId = request.UserId,
                UserUrl = request.UserUrl
            };
            databaseContext.Add(newRequest);

            await databaseContext.SaveChangesAsync();
            return newRequest;
        }

        public async Task ReadyResult(Request request, string createId, DatabaseContext databaseContext, Result readyResult)
        {
            // TODO

            var completeRequest = new Request
            {
                CreateId = request.CreateId,
                OwnerId = request.OwnerId,
                Social = request.Social,
                Status = Status.Complete,
                User = request.User,
                UserId = request.UserId,
                UserUrl = request.UserUrl,
                TimeComplete = DateTime.Now - request.DateTime
            };

            readyResult.Request = completeRequest;

            for (int i = 0; i < readyResult.ResultObjects.Count; i++)
            {
                var tmp = databaseContext.AnalysObjects.FirstOrDefault(x => x.Name == readyResult.ResultObjects[i].AnalysObject.Name);
                if (tmp == null)
                    databaseContext.AnalysObjects.Add(readyResult.ResultObjects[i].AnalysObject);
                else
                    readyResult.ResultObjects[i].AnalysObject = tmp;
            }

            var resultIndex = readyResult.ResultObjects.Sum(x => x.Count * x.AnalysObject.Weight);
            readyResult.ResultAnswer = resultIndex > 1;

            databaseContext.Add(completeRequest);
            databaseContext.Add(readyResult);
            databaseContext.SaveChanges();
        }
    }
}
