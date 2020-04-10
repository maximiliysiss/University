using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Models;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    public class AnaliticController : Controller
    {
        private readonly AnaliticService analiticService;

        public AnaliticController(AnaliticService analiticService)
        {
            this.analiticService = analiticService;
        }

        public IActionResult StartAnalys([FromForm]AnalitycsRequestModel analitycsRequest)
        {
            if (analiticService.CreateRequest(analitycsRequest))
                return RedirectToActionPermanent("Index", "Request");
            return BadRequest();
        }
    }
}