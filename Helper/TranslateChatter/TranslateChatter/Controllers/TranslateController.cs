using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TranslateChatter.Models;
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
        private readonly UserManager<User> userManager;

        public TranslateController(ITranslateService translateService, UserManager<User> userManager)
        {
            this.translateService = translateService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<string> MessageTranlsate([FromBody]TranslateViewModel translateView)
        {
            var user = await userManager.FindByEmailAsync(User.Identity.Name);
            var senderUser = await userManager.FindByEmailAsync(translateView.Sender);
            return await translateService.Translate(translateView.Message, user.Language.Code, senderUser.Language.Code);
        }
    }
}