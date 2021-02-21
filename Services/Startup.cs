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

            //� ������ ������� �� ������������� ����� � �������� ����� ���������
            //����� � ������������ �������� ����� IMessageSender
            services.AddTransient<IMessageSender, SmsMessageSender>();
            //��� ����� ����� ����������
            //services.AddSmsMessageSender();

            //� ���� ������ ������������� ����� �������� � �������
            //����� � ������������ �������� ����� EmailMessageSender
            //services.AddTransient<EmailMessageSender>();
            //��� ����� ����� ����������
            services.AddEmailMessageSender();

            //��� ���� ���������� ����� �������� ������ IMessageSrvice...
            //��� ��� �� ������������ ������ ������
            services.AddTransient<MessageSender>();

            services.AddTransient<Transient>(); //��� ������ ��������� � ������� ��������� ����� ������ ������� 
            services.AddScoped<Scoped>(); //��� ������� ������� ��������� ���� ������ �������
            services.AddSingleton<Singleton>(); //������ ������� ��������� ��� ������ ��������� � ����, ��� ����������� ������� ���������� ���� � ��� �� ����� ��������� ������ �������

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
