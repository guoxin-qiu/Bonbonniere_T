using Microsoft.AspNetCore.Builder;

namespace Bonbonniere.WebApis.Middlewares
{
    public static class BasicAuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
