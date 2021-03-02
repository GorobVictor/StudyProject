using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Provider
{
    [Microsoft.Extensions.Logging.ProviderAlias("File")]
    public class TxtFileLoggerProvider : ILoggerProvider
    {
        private string _filename { get; set; }

        public TxtFileLoggerProvider(string filename)
        {
            _filename = filename;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TxtFileLogger(_filename);
        }

        public void Dispose()
        {

        }
    }
}
