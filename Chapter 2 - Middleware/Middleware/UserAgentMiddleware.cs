using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class UserAgentMiddleware
    {
        private RequestDelegate _next { get; }

        public UserAgentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var useragents = httpContext.Request.Headers["User-Agent"];

            if (useragents.Count == 0)
            {
                httpContext.Response.StatusCode = 404;
                await httpContext.Response.WriteAsync("NotFound UserAgnet");
            }

            await _next.Invoke(httpContext);
        }
    }
}
