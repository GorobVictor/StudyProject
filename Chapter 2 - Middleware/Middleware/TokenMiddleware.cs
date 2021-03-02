using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class TokenMiddleware
    {
        private RequestDelegate _next { get; }
        private string _pattern { get; }

        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            this._next = next;
            this._pattern = pattern;
        }
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            var token = httpcontext.Request.Query["token"];

            if(token != _pattern)
            {
                httpcontext.Response.StatusCode = 404;
                await httpcontext.Response.WriteAsync("NotFound");
            }
            else
            {
                await _next.Invoke(httpcontext);
            }
        }
    }
}
