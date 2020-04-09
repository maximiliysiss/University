using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApisManager apisManager;

        public PeopleController(ApisManager apisManager)
        {
            this.apisManager = apisManager;
        }

        [HttpGet]
        public IActionResult Index([FromQuery]OpenPeopleViewModel openPeopleViewModel)
        {
            return View(apisManager[openPeopleViewModel.Social].GetUserDetailInformationView(openPeopleViewModel.Key));
        }
    }
}