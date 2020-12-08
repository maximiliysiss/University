using Microsoft.AspNetCore.Mvc;

namespace SiteCarAsp.Controllers
{
    /// <summary>
    /// Главный контроллер
    /// </summary>
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult AboutUs() => View();
        [HttpGet]
        public IActionResult Credits() => View();
    }
}
