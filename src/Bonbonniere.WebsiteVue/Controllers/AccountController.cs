using Bonbonniere.Infrastructure.Extensions;
using Bonbonniere.Infrastructure.Utilities.Helpers;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Services.Messaging.AccountService;
using Bonbonniere.WebsiteVue.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;

namespace Bonbonniere.WebsiteVue.Controllers
{
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string returnUrl = null)
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToLocal("/");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var checkLoginRequest = new CheckLoginRequest { Email = model.Email, Password = model.Password };
                var result = _accountService.CheckLogin(checkLoginRequest);
                if (result.Success)
                {
                    var accountInfo = _accountService.GetAccountInfo(new GetAccountInfoRequest { Email = model.Email });
                    HttpContext.SetAuthentication(accountInfo.User.Email, accountInfo.User.UserName, model.RememberMe);

                    var token = TokenHelper.GetToken(model.Email, model.Password); // TODO:
                    _memoryCache.Set(token, token, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(5)));

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.RemoveAuthentication();
            _memoryCache.Remove(HttpContext.Request.Headers["Authorization"]);

            return RedirectToLocal("/Account");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}