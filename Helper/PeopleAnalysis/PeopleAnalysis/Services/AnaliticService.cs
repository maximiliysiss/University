using PeopleAnalysis.ViewModels;
using System.Linq;

namespace PeopleAnalysis.Services
{
    public class AnaliticService
    {
        private readonly DatabaseContext databaseContext;

        public AnaliticService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public AnalitycsViewModel GetAnaliticsAboutUser(string userId, string social, int currentUserId)
        {
            var lastAnalitics = databaseContext.Requests.FirstOrDefault(x => x.UserId == userId && x.Social == social && x.OwnerId == currentUserId);
            if (lastAnalitics == null)
                return null;
            return new AnalitycsViewModel
            {
                Status = lastAnalitics.Status
            };
        }
    }
}
