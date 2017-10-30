using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Bonbonniere.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static void SetAuthentication(this HttpContext httpContext,
            string email, string fullName, bool isPersistent = false)
        {
            var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, fullName));
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, email));

            var userPrincipal = new ClaimsPrincipal(userIdentity);
            httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddMinutes(20),
                    IsPersistent = isPersistent,
                    AllowRefresh = false
                }).Wait();
        }

        public static void RemoveAuthentication(this HttpContext httpContext)
        {
            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        }
    }
}
