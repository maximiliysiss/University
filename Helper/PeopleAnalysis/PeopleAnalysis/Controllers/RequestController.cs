using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Extensions;
using PeopleAnalysis.Models;
using PeopleAnalysis.Services;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public RequestController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            List<Request> requests = new List<Models.Request>();
            IEnumerable<Request> request = databaseContext.Requests;
            if (!User.IsAdmin())
                request = request.Where(x => x.OwnerId == User.UserId());
            foreach (var r in request.AsEnumerable().GroupBy(x => new { x.UserId, x.Social, x.OwnerId }).ToDictionary(x => x.Key, x => x.OrderBy(x => x.DateTime)))
            {
                var res = r.Value.Last();
                res.DateTime = r.Value.First().DateTime;
                requests.Add(res);
            }
            return View(requests);
        }

        [HttpPost]
        public IActionResult Delete([FromForm]int toDelete)
        {
            var find = databaseContext.Requests.Find(toDelete);
            if (find == null)
                return NotFound();
            databaseContext.Add(new Request
            {
                CreateId = User.UserId(),
                OwnerId = find.OwnerId,
                Social = find.Social,
                Status = Status.Closed,
                User = find.User,
                UserId = find.UserId,
                UserUrl = find.UserUrl
            });
            databaseContext.SaveChanges();
            return Redirect("Index");
        }
    }
}