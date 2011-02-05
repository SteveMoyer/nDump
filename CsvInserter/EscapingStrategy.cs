using System;

namespace CsvInserter
{
    public  class EscapingStrategy
    {
        private readonly char _quoteChar;
        private const string SingleSingleQuote = "'";
        private const string DoubleSingleQuote = "''";
        
        public EscapingStrategy( char quoteChar)
        {
            _quoteChar = quoteChar;
        }

        public string EscapeWithQuote(string value)
        {
            return (IsNullEmptyOrNullString(value)
                        ? NullString
                        : (_quoteChar + value + _quoteChar));
        }


        private bool IsNullEmptyOrNullString(string thisvalue)
        {
            return (string.IsNullOrEmpty(thisvalue) || thisvalue.Equals(NullString, StringComparison.OrdinalIgnoreCase));
        }


        public string EscapeWithoutQuote(string value)
        {
            return (string.IsNullOrEmpty(value) ? NullString : value);
        }
        private const string NullString = "null";

        public string Escape(string value, bool addQuotes)
        {
            var escapedString = value.Replace(SingleSingleQuote, DoubleSingleQuote);
            if (addQuotes)
                return EscapeWithQuote(escapedString);
            return EscapeWithoutQuote(escapedString);
        }
    }
}