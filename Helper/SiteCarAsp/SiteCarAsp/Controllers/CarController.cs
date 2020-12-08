using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiteCarAsp.Services;
using SiteCarAsp.ViewModels;

namespace SiteCarAsp.Controllers
{
    /// <summary>
    /// Контроллер для машин
    /// </summary>
    public class CarController : Controller
    {
        /// <summary>
        /// Сервис машин
        /// </summary>
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        /// <summary>
        /// Страница все машины
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index() => View(new CarInformationViewModel(await carService.GetCars()));

        /// <summary>
        /// Render страницы при запросе фильтрации
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FilterCars([FromBody] FilterViewModel[] filters)
        {
            return PartialView("~/Views/Components/CarTable.partial.cshtml", await carService.GetFilteredCars(filters));
        }


        [HttpGet]
        public async Task<IActionResult> UsedCars() => View("Index", new CarInformationViewModel(await carService.GetUsedCars()) { ActiveFilters = new[] { new FilterViewModel { Field = "used", Val = "true" } } });
        [HttpGet]
        public async Task<IActionResult> NewCars() => View("Index", new CarInformationViewModel(await carService.GetNewCars()) { ActiveFilters = new[] { new FilterViewModel { Field = "used", Val = "false" } } });
    }
}
