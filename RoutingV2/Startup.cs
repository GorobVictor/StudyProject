using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingV2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.Routes.Add(new AdminRoute());

            routeBuilder.MapRoute("{controller}/{action}",
                async context =>
                {
                    context.Response.ContentType = "text/html;charset=utf-8";
                    await context.Response.WriteAsync("двухсегментный запрос");
                });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }

    public class AdminRoute : IRouter
    {
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {

            /* result: Привет admin!
            string url = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if (url.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                context.Handler = async ctx =>
                {
                    ctx.Response.ContentType = "text/html;charset=utf-8";
                    await ctx.Response.WriteAsync("Привет admin!");
                };
            }
            await Task.CompletedTask;
            */

            // result: Привет admin!Hello World!

            string url = context.HttpContext.Request.Path.Value.TrimEnd('/');
            if (url.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                await context.HttpContext.Response.WriteAsync("Привет admin!");
            }
        }
    }
}
