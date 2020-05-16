using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAnalysis.Extensions;
using PeopleAnalysis.Services;
using PeopleAnalysis.ViewModels;

namespace PeopleAnalysis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnaliticController : ControllerBase
    {
        private readonly AnaliticService analiticService;

        public AnaliticController(AnaliticService analiticService)
        {
            this.analiticService = analiticService;
        }

        [HttpPost("StartAnalys")]
        public ActionResult<bool> StartAnalys([FromBody]AnalitycsRequestModel analitycsRequest)
        {
            return analiticService.CreateRequest(analitycsRequest, User.UserId());
        }
    }
}