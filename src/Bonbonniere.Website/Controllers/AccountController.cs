﻿using Bonbonniere.Infrastructure.Extensions;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Services.Messaging.AccountService;
using Bonbonniere.Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bonbonniere.Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(LoginViewModel model, string returnUrl = null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignOut()
        {
            HttpContext.RemoveAuthentication();

            return RedirectToLocal("/");
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