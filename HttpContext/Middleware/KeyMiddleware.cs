using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpContext.Middleware
{
    public class KeyMiddleware
    {
        private RequestDelegate _next { get; }
        public KeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext http)
        {
            http.Items["key"] = http.Request.Query["token"];
            await _next.Invoke(http);
        }
    }
}
