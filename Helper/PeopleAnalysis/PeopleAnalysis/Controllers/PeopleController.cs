using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
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
            detail.AnalitycsViewModel = analiticService.GetAnaliticsAboutUser(detail.Id, detail.Social, 0);
            return View(detail);
        }
    }
}