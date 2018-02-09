using Microsoft.AspNetCore.Builder;

namespace Bonbonniere.WebsiteVue.MvcMiddlewares
{
    public static class BasicAuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
