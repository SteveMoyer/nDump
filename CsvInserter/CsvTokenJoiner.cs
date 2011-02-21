namespace CsvInserter
{
    public class CsvTokenJoiner
    {
        private readonly string _delimeter;
        private const string Delimeter = ",";

        private const char QuoteChar = '\'';

        public CsvTokenJoiner(string delimeter)
        {
            _delimeter = delimeter;
        }

        public CsvTokenJoiner():this(Delimeter)
        {

        }

        public string Join(string[] values)
        {
            return Join(values, false, ' ');
        }

        public string Join(string[] values,  bool addQuotes, char quoteChar)
        {
            var joinedValues = "";
            var delimeterIfNotFirstValue = "";
            var escapingStrategy = new EscapingStrategy(quoteChar);
            foreach (string value in values)
            {
                joinedValues = joinedValues + delimeterIfNotFirstValue + escapingStrategy.Escape(value, addQuotes);
                delimeterIfNotFirstValue = _delimeter;
            }
            return joinedValues;
        }

        public string JoinValues(string[] values)
        {
            return Join(values,  true, QuoteChar);
        }
        
    }
}