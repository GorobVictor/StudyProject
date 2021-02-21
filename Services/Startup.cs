using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Middleware;
using Services.Services;
using Services.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class Startup
    {
        public static IServiceCollection Services { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            Services = services;

            //В данном случаем мы устанавливаем связь с сервисом через интерфейс
            //можно в конструкторе вызывать через IMessageSender
            services.AddTransient<IMessageSender, SmsMessageSender>();
            //или через метод расширения
            //services.AddSmsMessageSender();

            //В этом случае устанавливаем связь напрямую с классов
            //можно в конструкторе вызывать через EmailMessageSender
            //services.AddTransient<EmailMessageSender>();
            //или через метод расширения
            services.AddEmailMessageSender();

            //Для этой реализации нужно добавить сервис IMessageSrvice...
            //так как он используется внутри класса
            services.AddTransient<MessageSender>();

            services.AddTransient<Transient>(); //при каждом обращении к сервису создается новый объект сервиса 
            services.AddScoped<Scoped>(); //для каждого запроса создается свой объект сервиса
            services.AddSingleton<Singleton>(); //объект сервиса создается при первом обращении к нему, все последующие запросы используют один и тот же ранее созданный объект сервиса

            services.AddRazorPages();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender messageSender)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<ScopedTestMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
