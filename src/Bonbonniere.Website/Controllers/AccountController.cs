using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bonbonniere.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Bonbonniere.Website.ViewModels;
using Bonbonniere.Infrastructure.Extensions;

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
                var result = _accountService.CheckSignIn(model.Email, model.Password);
                if (result)
                {
                    HttpContextExtensions.SetAuthentication(model.Email, model.RememberMe); // TODO
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
            HttpContextExtensions.RemoveAuthentication(); // TODO:
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