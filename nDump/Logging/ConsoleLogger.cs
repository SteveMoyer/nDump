using System;

namespace nDump.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(String message)
        {
            Console.WriteLine(message);
        }
        public void Log(Exception ex)
        {
            while (ex != null)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                ex = ex.InnerException;
            }
        }
    }
}