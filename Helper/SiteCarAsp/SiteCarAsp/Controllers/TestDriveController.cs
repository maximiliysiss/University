using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteCarAsp.Models;
using SiteCarAsp.Services;
using SiteCarAsp.ViewModels;
using System.Threading.Tasks;

namespace SiteCarAsp.Controllers
{
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
        public async Task<IActionResult> Index([FromQuery] int carId) => View(new TestDriveViewModel(await testDriveService.GetCarsAsync(), carId));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TestDriveViewModel createTestDrive)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new TestDriveViewModel(await testDriveService.GetCarsAsync(), createTestDrive.CarId)
                {
                    Fio = createTestDrive.Fio,
                    Phone = createTestDrive.Phone
                });
            }
            await testDriveService.AddTestDriveAsync(mapper.Map<TestDrive>(createTestDrive));
            return RedirectToAction("Index");
        }
    }
}
