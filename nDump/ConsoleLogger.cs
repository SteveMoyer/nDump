using System;

namespace nDump
{
    public class ConsoleLogger : ILogger
    {
        public void Log(String message)
        {
            Console.WriteLine(message);
        }
    }
}