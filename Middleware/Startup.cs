using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Middleware.Middleware;
using Middleware.Middleware.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //если мидлвар наследывается от интерфейса IMiddleware то нужно его зарегистрировать
            services.AddTransient<HostMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //используем метод расширения для вызова
            app.UseHost();

            //app.UseMiddleware<HostMiddleware>();

            //используем вызов класса без метода расширения
            app.UseMiddleware<UserAgentMiddleware>();

            //app.UseToken("1564");
            app.UseMiddleware<TokenMiddleware>("123456");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
