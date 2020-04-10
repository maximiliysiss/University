using PeopleAnalysis.Models;
using PeopleAnalysis.ViewModels;
using System;
using System.Linq;

namespace PeopleAnalysis.Services
{
    public class AnaliticService
    {
        private readonly DatabaseContext databaseContext;
        private readonly ApisManager apisManager;

        public AnaliticService(DatabaseContext databaseContext, ApisManager apisManager)
        {
            this.databaseContext = databaseContext;
            this.apisManager = apisManager;
        }

        public AnalitycsViewModel GetAnaliticsAboutUser(string userId, string social, int currentUserId)
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

        public bool CreateRequest(AnalitycsRequestModel analitycsRequest)
        {
            // TODO create view
            var isExists = databaseContext.Requests.Where(x => x.OwnerId == 0 && x.Social == analitycsRequest.Social && x.User == analitycsRequest.Id)
                .GroupBy(x => x.Session).Where(x => !x.Any(y => y.Status == Status.Closed || y.Status == Status.Fail)).Any();
            if (isExists)
                return false;
            databaseContext.Requests.Add(new Request
            {
                CreateId = 0,
                OwnerId = 0,
                Status = Status.Create,
                User = analitycsRequest.UserName,
                Social = analitycsRequest.Social,
                UserId = analitycsRequest.Id,
                UserUrl = apisManager[analitycsRequest.Social].GetUserUri(analitycsRequest.Id),
                Session = Guid.NewGuid()
            });
            databaseContext.SaveChanges();
            return true;
        }
    }
}
