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
                    using (var stream = new StreamWriter(_filePath.Replace("{date}", DateTime.Now.ToString("yyyy-MM-dd")), true, Encoding.UTF8))
                    {
                        string color;
                        switch (logLevel)
                        {
                            case LogLevel.Information:
                                color = "green";
                                break;
                            case LogLevel.Error:
                                color = "darkred";
                                break;
                            case LogLevel.Critical:
                                color = "red";
                                break;
                            case LogLevel.Debug:
                                color = "dimgrey";
                                break;
                            case LogLevel.Warning:
                                color = "yellow";
                                break;
                            default:
                                color = "black";
                                break;
                        }
                        stream.WriteLine($"<h3 style=\"color: {color}; background-color: black;\">{state} <p1 style=\"color: white;\"> - {DateTime.Now}</p1></h3>");
                        stream.WriteLine($"<p1>{exception?.Message}</p1><br>");
                        stream.WriteLine($"<p1>{exception?.StackTrace}</p1><br>");
                        stream.WriteLine($"<p1>{exception?.InnerException?.Message}</p1><br>");
                        stream.WriteLine($"<p1>{exception?.InnerException?.StackTrace}</p1><br>");
                        stream.WriteLine($"<p1>{new string('-', 100)}</p1><br>");
                    }
                }
            }
        }
    }
}
