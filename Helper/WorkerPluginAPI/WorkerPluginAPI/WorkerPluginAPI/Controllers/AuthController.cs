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
        /// Авторизация
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
        /// Обновить токен
        /// </summary>
        /// <param name="token"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<TokenResult> Refresh([FromHeader]string authorization, [FromHeader]string refreshToken)
        {
            if (StringUtils.IsNullOrEmpty(authorization, refreshToken))
                return NotFound();

            var refreshResult = authService.RefreshToken(authorization.Split(" ")[1], refreshToken);
            if (refreshResult == null)
                return BadRequest();

            return refreshResult;
        }

        /// <summary>
        /// Авторизация по токену
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Login() => Ok();
    }
}