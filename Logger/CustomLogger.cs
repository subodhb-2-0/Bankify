using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logger
{
    public class CustomLogger : ICustomLogger
    {
        public CustomLogger()
        {
            Log.Logger = new LoggerConfiguration()
                         //.WriteTo.Console(new JsonFormatter())
                         //.WriteTo.Seq("http://localhost:5341")

                         .WriteTo.File(@".\logs\log.txt", rollingInterval: RollingInterval.Day)
                         //.WriteTo.Debug()
                         .CreateLogger();
                         

            Log.Information("Starting web host");

        }

        public void LogInfo(string message)
        {
            Log.Information(message);
        }

        public void LogObject(object _object)
        {
            string _logData = JsonSerializer.Serialize(_object);
            Log.Information(_logData);
        }

        public void LogException(Exception ex)
        {
            Log.Fatal(ex.Message + ";" + ex.InnerException + "Stacktrace:" + ex.StackTrace, Encoding.Default);
        }

        public void LogDebug(string message)
        {
            Log.Debug(message);
        }

        public void LogTrace(string message)
        {
            Log.Verbose(message);
        }
    }
}
