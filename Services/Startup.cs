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