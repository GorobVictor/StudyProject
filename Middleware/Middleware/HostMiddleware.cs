using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class HostMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var host = context.Request.Headers["Host"];
            if(host.Count==0)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Bad Host");
            }

            await next(context);
        }
    }
}
