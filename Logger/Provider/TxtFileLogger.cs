using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Provider
{
    public class TxtFileLogger : ILogger
    {
        private string _filePath;
        private static object _lock = new object();
        public TxtFileLogger(string filepath)
        {
            _filePath = filepath;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    using (var stream = new StreamWriter(_filePath, true, Encoding.UTF8))
                    {
                        string color;
                        switch (logLevel)
                        {
                            case LogLevel.Information:
                                color = "green";
                                break;
                            case LogLevel.Error:
                                color = "red";
                                break;
                            default:
                                color = "black";
                                break;
                        }
                        stream.WriteLine($"<h3 style=\"color: {color};\">{state}</h3>");
                        stream.WriteLine($"<p1>{formatter(state, exception)}</p1><br>");
                        stream.WriteLine($"<p1>{new string('-', 100)}</p1><br>");
                    }
                }
            }
        }
    }
}
