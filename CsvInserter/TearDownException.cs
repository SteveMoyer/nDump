using System;

namespace nDump
{
    public class TearDownException : Exception
    {
        public TearDownException(string message)
            : base(message)
        {
        }
    }
}