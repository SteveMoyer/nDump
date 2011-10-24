using System;
using System.Linq;

namespace nDump.Transformation.Escaping
{
    public  class ValueEscapingStrategy:IEscapingStrategy 
    {
        private const string SingleSingleQuote = "'";
        private const string DoubleSingleQuote = "''";
        private const string NullString = "null";


        private string EscapeWithQuote(string value)
        {
            return (IsNullEmptyOrNullString(value)
                        ? NullString
                        : ("N" + SingleSingleQuote + value + SingleSingleQuote));
        }


        private bool IsNullEmptyOrNullString(string thisvalue)
        {
            return (string.IsNullOrEmpty(thisvalue) || thisvalue.Equals(NullString, StringComparison.OrdinalIgnoreCase));
        }



        public string Escape(string value)
        {
            var escapedString = value==null? value: value.Replace(SingleSingleQuote, DoubleSingleQuote);
            return EscapeWithQuote(escapedString);
            
        }

        public string[] Escape(string[] values)
        {
            return values.Select(value => Escape(value)).ToArray();
        }
    }
}