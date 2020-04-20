using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslateChatter.AuthAPI;
using TranslateChatter.Extensions;
using TranslateChatter.Services;
using TranslateChatter.ViewModels;

namespace TranslateChatter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslateService translateService;
        private readonly IAuthAPIClient authAPIClient;

        public TranslateController(ITranslateService translateService, IAuthAPIClient authAPIClient)
        {
            this.translateService = translateService;
            this.authAPIClient = authAPIClient;
        }

        [HttpPost]
        public async Task<string> MessageTranlsate([FromBody]TranslateViewModel translateView)
        {
            var senderUser = await authAPIClient.ApiUserFindAsync(translateView.Sender);
            return await translateService.Translate(translateView.Message, User.Code(), senderUser.Language.Code);
        }
    }
}