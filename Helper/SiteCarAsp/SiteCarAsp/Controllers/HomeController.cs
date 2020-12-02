using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteCarAsp.Services;
using SiteCarAsp.ViewModels;

namespace SiteCarAsp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService carService;

        public HomeController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(new CarInformationViewModel(await carService.GetCars()));
        [HttpGet]
        public async Task<IActionResult> UsedCars() => View("Index", new CarInformationViewModel(await carService.GetUsedCars()) { ActiveFilters = new[] { new FilterViewModel { Field = "used", Val = "true" } } });
        [HttpGet]
        public async Task<IActionResult> NewCars() => View("Index", new CarInformationViewModel(await carService.GetNewCars()) { ActiveFilters = new[] { new FilterViewModel { Field = "used", Val = "false" } } });

        [HttpPost]
        public async Task<IActionResult> FilterCars([FromBody] FilterViewModel[] filterViewModels)
        {
            var filters = filterViewModels.GroupBy(x => x.Field).ToDictionary(x => x.Key, x => string.Join(",", x.Select(x => x.Val)));
            return PartialView("~/Views/Components/_CarTable.cshtml", await carService.GetFilteredCars(filters));
        }

        [HttpGet]
        public IActionResult AboutUs() => View();
        [HttpGet]
        public IActionResult Credits() => View();
    }
}
