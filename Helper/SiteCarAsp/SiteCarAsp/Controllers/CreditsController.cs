using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteCarAsp.Services.Controller;
using SiteCarAsp.ViewModels;

namespace SiteCarAsp.Controllers
{
    public class CreditsController : Controller
    {
        private readonly ICreditsService creditsService;

        public CreditsController(ICreditsService creditsService)
        {
            this.creditsService = creditsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(new CreditsViewModel(await creditsService.GetCarsAsync()));

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            return View("Index");
        }
    }
}
