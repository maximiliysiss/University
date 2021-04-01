using Microsoft.AspNetCore.Mvc;
using RockShop.Models;
using RockShop.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RockShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRockService rockService;

        public HomeController(IRockService rockService)
        {
            this.rockService = rockService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await rockService.GetRocksAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
