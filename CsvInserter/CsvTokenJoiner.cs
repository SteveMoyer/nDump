namespace CsvInserter
{
    public class CsvTokenJoiner
    {
        private readonly string _delimeter;
        private const string Delimeter = ",";


        public CsvTokenJoiner(string delimeter)
        {
            _delimeter = delimeter;
        }

        public CsvTokenJoiner():this(Delimeter)
        {

        }

        public string Join(string[] values)
        {
            var joinedValues = "";
            var delimeterIfNotFirstValue = "";
            foreach (string value in values)
            {
                joinedValues = joinedValues + delimeterIfNotFirstValue + value;
                delimeterIfNotFirstValue = _delimeter;
            }
            return joinedValues;
        }
    }
}