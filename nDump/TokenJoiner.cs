namespace nDump
{
    public class TokenJoiner
    {
        private readonly string _delimeter;
        private const string Delimeter = ",";


        public TokenJoiner(string delimeter)
        {
            _delimeter = delimeter;
        }

        public TokenJoiner():this(Delimeter)
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