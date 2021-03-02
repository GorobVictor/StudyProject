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

namespace Routing
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
                options.ConstraintMap.Add("position", typeof(PositionConstraint)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                /*
                 если заданного машрута не существуте, то endpoint равен null
                 */
                Endpoint endpoint = context.GetEndpoint();
                if(endpoint==null)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync("NotFound");
                }
                await next();
            });

            var myRouteHandler = new RouteHandler(HandleAsync);

            var routeBuilder = new RouteBuilder(app, myRouteHandler);
            routeBuilder.MapRoute(
                "default",
                "{controller}/{action}/{id:position?}");
            app.UseRouter(routeBuilder.Build());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/index", async context =>
                {
                    await context.Response.WriteAsync("Hello World Index!");
                });
            });
        }
        private async Task HandleAsync(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var action = routeValues["action"].ToString();
            var controller = routeValues["controller"].ToString();
            string id = routeValues["id"]?.ToString();
            await context.Response.WriteAsync($"controller: {controller} | action: {action} | id: {id}");
        }
    }
    public class PositionConstraint : IRouteConstraint
    {
        string[] positions = new[] { "admin", "director", "accountant" };
        public bool Match(HttpContext httpContext, IRouter route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            return positions.Contains(values[routeKey]?.ToString().ToLowerInvariant());
        }
    }
}
