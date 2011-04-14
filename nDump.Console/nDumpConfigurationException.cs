using System;

namespace nDump.Console
{
    internal class nDumpConfigurationException : Exception
    {
        public nDumpConfigurationException(string message):base(message)
        {
            
        }

        public nDumpConfigurationException()
        {
        }
    }
}