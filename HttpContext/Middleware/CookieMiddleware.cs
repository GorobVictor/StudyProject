using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpContext.Middleware
{
    public class CookieMiddleware
    {
        private RequestDelegate _next { get; }
        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext http)
        {
            if (!http.Request.Cookies.ContainsKey("time"))
                http.Response.Cookies.Append("time", DateTime.Now.ToString());
            else if(Convert.ToDateTime(http.Request.Cookies["time"])!= DateTime.Now)
                http.Response.Cookies.Append("time", DateTime.Now.ToString());
            await _next.Invoke(http);
        }
    }
}
