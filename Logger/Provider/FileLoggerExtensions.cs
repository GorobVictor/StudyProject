using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Provider
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddTxtFile(this ILoggerFactory factory, string filename)
        {
            factory.AddProvider(new TxtFileLoggerProvider(filename));
            return factory;
        }
    }
}
