using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAnalysis.Models;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApisManager apisManager;

        public HomeController(ILogger<HomeController> logger, ApisManager apisManager)
        {
            _logger = logger;
            this.apisManager = apisManager;
        }

        public IActionResult Index()
        {
            return View(new FindPeoplePageViewModel());
        }

        [HttpPost]
        public IActionResult Index([FromForm]FindPeoplePageViewModel findPeoplePageViewModel)
        {
            if (!string.IsNullOrEmpty(findPeoplePageViewModel.FindPeopleViewModel.FindText))
                findPeoplePageViewModel.FinderResultViewModel = apisManager.GetFinded(findPeoplePageViewModel.FindPeopleViewModel.FindText);
            return View(findPeoplePageViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
