using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteCarAsp.Models;
using SiteCarAsp.Services;
using SiteCarAsp.ViewModels;
using SiteCarAsp.ViewModels.Request;
using System.Threading.Tasks;

namespace SiteCarAsp.Controllers
{
    /// <summary>
    /// Контроллер для TestDrive
    /// </summary>
    public class TestDriveController : Controller
    {
        private readonly ITestDriveService testDriveService;
        private readonly IMapper mapper;

        public TestDriveController(ITestDriveService testDriveService, IMapper mapper)
        {
            this.testDriveService = testDriveService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int carId) => View(new TestDriveViewModel { CarId = carId, CarInformations = await testDriveService.GetCarsAsync() });

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTestDriveRequest createTestDrive)
        {
            if (!ModelState.IsValid)
            {
                var model = mapper.Map<TestDriveViewModel>(createTestDrive);
                model.CarInformations = await testDriveService.GetCarsAsync();
                return View("Index", model);
            }
            await testDriveService.AddTestDriveAsync(mapper.Map<TestDrive>(createTestDrive));
            return RedirectToAction("Index");
        }
    }
}
