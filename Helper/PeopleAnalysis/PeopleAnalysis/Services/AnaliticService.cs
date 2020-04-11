using PeopleAnalysis.Models;
using PeopleAnalysis.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAnalysis.Services
{
    public class AnaliticService
    {
        private readonly DatabaseContext databaseContext;
        private readonly ApisManager apisManager;
        private readonly ISender sender;

        public AnaliticService(DatabaseContext databaseContext, ApisManager apisManager, ISender sender)
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
            return new AnalitycsViewModel
            {
                Status = lastAnalitics.Status
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
            databaseContext.Requests.Add(newRequest);
            databaseContext.SaveChanges();
            sender.Send(newRequest);
            return true;
        }
    }

    public interface IAnaliticAIService
    {
        Task InProcessAsync(Request request, string user, DatabaseContext databaseContext);
    }

    public class AnaliticAIService : IAnaliticAIService
    {
        public async Task InProcessAsync(Request request, string user, DatabaseContext databaseContext)
        {
            var find = databaseContext.Requests.Find(request.Id);
            if (find == null)
                throw new ApplicationException("Not found request");
            if (find.Status != request.Status)
                throw new ApplicationException("Request is changed");

            databaseContext.Add(new Request
            {
                CreateId = user,
                OwnerId = request.OwnerId,
                Social = request.Social,
                Status = Status.InProgress,
                User = request.User,
                UserId = request.UserId,
                UserUrl = request.UserUrl
            });

            await databaseContext.SaveChangesAsync();
        }
    }
}
