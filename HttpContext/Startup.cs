using HttpContext.Middleware;
using HttpContext.Models;
using HttpContext.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpContext
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "session";
                options.IdleTimeout = TimeSpan.FromSeconds(5);
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<KeyMiddleware>();
            app.UseMiddleware<CookieMiddleware>();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var command = $"{context.Items["key"]}\n";

                    if (context.Session.Keys.Contains("name"))
                        command += context.Session.GetObject<User>("name");
                    else
                        context.Session.SetObject<User>(new User()
                        {
                            Name = "Victor",
                            Age = 25
                        },
                        "name");

                    await context.Response.WriteAsync(command);
                });
            });
        }
    }
}
