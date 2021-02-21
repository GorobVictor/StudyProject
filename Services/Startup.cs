using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
