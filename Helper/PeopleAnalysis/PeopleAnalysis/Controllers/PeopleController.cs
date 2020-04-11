using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Extensions;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly ApisManager apisManager;
        private readonly AnaliticService analiticService;

        public PeopleController(ApisManager apisManager, AnaliticService analiticService)
        {
            this.apisManager = apisManager;
            this.analiticService = analiticService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery]OpenPeopleViewModel openPeopleViewModel)
        {
            var detail = apisManager[openPeopleViewModel.Social].GetUserDetailInformationView(openPeopleViewModel.Key);
            detail.AnalitycsViewModel = analiticService.GetAnaliticsAboutUser(detail.Id, detail.Social, User.UserId());
            return View(detail);
        }
    }
}