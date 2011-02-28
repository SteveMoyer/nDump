using System.Collections.Generic;

namespace CsvInserter
{
    public class SqlTableSelect
    {
        private  List<string> _excludedColumns = new List<string>();
        private  string _tableName;
        private  string _select;
        private  bool _hasIdentity;

        public SqlTableSelect()
        {
        }

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
            set { _excludedColumns = value; }
        }

        public bool HasIdentity
        {
            get { return _hasIdentity; }
            set { _hasIdentity = value; }
        }

        public string Select
        {
            get { return _select; }
            set { _select = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
    }
}