using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.ApplicationAPI;
using System.Threading.Tasks;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    public class AnaliticController : Controller
    {
        private readonly IApplicationAPIClient applicationAPIClient;

        public async Task<IActionResult> StartAnalys([FromForm]AnalitycsRequestModel analitycsRequest)
        {
            if (await applicationAPIClient.ApiAnaliticStartanalysAsync(analitycsRequest))
                return RedirectToActionPermanent("Index", "Request");
            return BadRequest();
        }
    }
}