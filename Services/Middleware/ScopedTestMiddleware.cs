using Microsoft.AspNetCore.Http;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Middleware
{
    public class ScopedTestMiddleware
    {
        private RequestDelegate next { get; }

        public ScopedTestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, Scoped scoped)
        {
            scoped.GetIndex();
            await next.Invoke(context);
        }
    }
}
