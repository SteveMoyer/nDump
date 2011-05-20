using System.Linq;

namespace nDump
{
    public class ColumnHeaderKeywordEscapingStrategy : IEscapingStrategy
    {
        private readonly string[] _keyWords = new[] {"user", "group", "database"};
        private readonly string[] _nonsense = new[] {" ", "?", "/"};

        public string Escape(string value)
        {
            var hasNonsense = _nonsense.Any(x => value.Contains(x));
            var isKeyword = _keyWords.Contains(value.ToLower());
            return (isKeyword || hasNonsense) ? "[" + value + "]" : value;
        }

        public string[] Escape(string[] values)
        {
            return values.Select(name => Escape(name)).ToArray();
        }
    }
}