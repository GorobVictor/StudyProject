using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.Extensions
{
    public static class MessageSenderExtensions
    {
        public static void AddSmsMessageSender(this IServiceCollection services)
        {
            services.AddTransient<IMessageSender, SmsMessageSender>();
        }
        public static void AddEmailMessageSender(this IServiceCollection services)
        {
            services.AddTransient<EmailMessageSender>();
        }
    }
}
