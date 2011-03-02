using System.Linq;

namespace nDump
{
    public class ColumnHeaderKeywordEscapingStrategy: IEscapingStrategy
    {
        private string[] _keyWords= new[] {"user", "group", "database"};

        public string Escape(string value)
        {
            return _keyWords.Contains(value.ToLower()) ? "[" + value + "]" : value;
        }

        public string[] Escape(string[] values)
        {

            return values.Select(name => Escape(name)).ToArray();
        }

        
    }
}