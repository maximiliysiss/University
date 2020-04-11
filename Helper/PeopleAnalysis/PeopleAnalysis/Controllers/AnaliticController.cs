using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    public class AnaliticController : Controller
    {
        private readonly AnaliticService analiticService;

        public AnaliticController(AnaliticService analiticService)
        {
            this.analiticService = analiticService;
        }

        public IActionResult StartAnalys([FromForm]AnalitycsRequestModel analitycsRequest)
        {
            if (analiticService.CreateRequest(analitycsRequest))
                return RedirectToActionPermanent("Index", "Request");
            return BadRequest();
        }
    }
}