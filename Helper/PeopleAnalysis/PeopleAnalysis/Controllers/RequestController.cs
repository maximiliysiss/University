using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Services;

namespace PeopleAnalysis.Controllers
{
    public class RequestController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public RequestController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            List<Models.Request> requests = new List<Models.Request>();
            foreach (var r in databaseContext.Requests.AsEnumerable().GroupBy(x => new { x.UserId, x.Social, x.OwnerId }).ToDictionary(x => x.Key, x => x.OrderBy(x => x.DateTime)))
            {
                var res = r.Value.Last();
                res.DateTime = r.Value.First().DateTime;
                requests.Add(res);
            }
            return View(requests);
        }
    }
}