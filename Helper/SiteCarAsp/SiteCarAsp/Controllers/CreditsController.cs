using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SiteCarAsp.Models;
using SiteCarAsp.Services.Controller;
using SiteCarAsp.ViewModels;
using SiteCarAsp.ViewModels.Request;

namespace SiteCarAsp.Controllers
{
    /// <summary>
    /// Контроллер для кредитов
    /// </summary>
    public class CreditsController : Controller
    {
        /// <summary>
        /// Сервис для кредитов
        /// </summary>
        private readonly ICreditsService creditsService;
        private readonly IMapper mapper;

        public CreditsController(ICreditsService creditsService, IMapper mapper)
        {
            this.creditsService = creditsService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить основную Viewху
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index() => View(new CreditsViewModel { CarInformations = await creditsService.GetCarsAsync() });

        /// <summary>
        /// Создать запрос на кредит
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCreditRequest createCreditRequest)
        {
            if (!ModelState.IsValid)
            {
                var model = mapper.Map<CreditsViewModel>(createCreditRequest);
                model.CarInformations = await creditsService.GetCarsAsync();
                return View("Index", model);
            }

            await creditsService.AddAsync(mapper.Map<Credit>(createCreditRequest));

            return RedirectToAction("Index");
        }
    }
}
