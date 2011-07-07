using System;

namespace nDump.Logging
{
    public class NullLogger : ILogger
    {
        public void Log(string message)
        {
            
        }

        public void Log(Exception ex)
        {
            
        }
    }
}