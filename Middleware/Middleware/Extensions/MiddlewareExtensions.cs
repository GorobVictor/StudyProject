using Microsoft.AspNetCore.Builder;

namespace Middleware.Middleware.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder applicationBuilder, string pattern)
        {
            return applicationBuilder.UseMiddleware<TokenMiddleware>(pattern);
        }
        public static IApplicationBuilder UseHost(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HostMiddleware>();
        }
    }
}
