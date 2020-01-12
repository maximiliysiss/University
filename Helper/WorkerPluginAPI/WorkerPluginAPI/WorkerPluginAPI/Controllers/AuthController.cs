﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerPluginAPI.Extensions;
using WorkerPluginAPI.Models.Controllers;
using WorkerPluginAPI.Services.AuthAPI.Services;

namespace WorkerPluginAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Login by login/password
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<TokenResult> Login(LoginModel loginModel)
        {
            var loginResult = authService.AuthAttempt(loginModel);
            if (loginResult == null)
                return NotFound("Неправильный логин/пароль");
            return loginResult;
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<TokenResult> Refresh([FromHeader]string token, [FromHeader]string refreshToken)
        {
            if (StringUtils.IsNullOrEmpty(token, refreshToken))
                return NotFound();

            var refreshResult = authService.RefreshToken(token, refreshToken);
            if (refreshResult == null)
                return BadRequest();

            return refreshResult;
        }

        /// <summary>
        /// Login by token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Login() => Ok();
    }
}