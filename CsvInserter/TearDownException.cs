using System;

namespace CsvInserter
{
    public class TearDownException : Exception
    {
        public TearDownException(string message)
            : base(message)
        {
        }
    }
}