using Bonbonniere.Infrastructure.Extensions;
using Bonbonniere.Infrastructure.Utilities.Helpers;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Services.Messaging.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace Bonbonniere.WebApis.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IMemoryCache memoryCache,
            IAccountService accountService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string email,string password, bool rememberme)
        {
            var checkLoginRequest = new CheckLoginRequest { Email = email, Password = password };
            var result = _accountService.CheckLogin(checkLoginRequest);
            if (result.Success)
            {
                var accountInfo = _accountService.GetAccountInfo(new GetAccountInfoRequest { Email = email });
                HttpContext.SetAuthentication(accountInfo.User.Email, accountInfo.User.UserName, rememberme);

                var token = TokenHelper.GetToken(email, password); // TODO:
                _memoryCache.Set(token, token, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(5)));

                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.RemoveAuthentication();
            _memoryCache.Remove(HttpContext.Request.Headers["Authorization"]);

            return Ok();
        }
    }
}
