using System.Collections.Generic;

namespace CsvInserter
{
    public class SqlTableSelect
    {
        private readonly List<string> _excludedColumns = new List<string>();
        private readonly string _tableName;
        private readonly string _select;
        private readonly bool _hasIdentity;

        public SqlTableSelect(string tableName, string select, bool hasIdentity)
        {
            _tableName = tableName;
            _select = select;
            _hasIdentity = hasIdentity;
        }

        public SqlTableSelect(string tableName, string select, bool hasIdentity, List<string> excludedColumns)
            : this(tableName, select, hasIdentity)
        {
            _excludedColumns = excludedColumns;
        }

        public List<string> ExcludedColumns
        {
            get { return _excludedColumns; }
        }

        public bool HasIdentity
        {
            get { return _hasIdentity; }
        }

        public string Select
        {
            get { return _select; }
        }

        public string TableName
        {
            get { return _tableName; }
        }
    }
}