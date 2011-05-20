using System.Collections.Generic;

namespace nDump
{
    public class SqlTableSelect
    {
        public override string ToString()
        {
            return _tableName;
        }
        private  bool _deleteOnly;
        private  List<string> _excludedColumns = new List<string>();
        private  string _tableName;
        private  string _select;
        private  bool _hasIdentity;

        public SqlTableSelect()
        {
        }


        public SqlTableSelect(string tableName,bool deleteOnly = false): this(tableName,string.Empty,false)
        {
            _deleteOnly = deleteOnly;
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

        public bool DeleteOnly
        {
            get { return _deleteOnly; }
            set { _deleteOnly = value; }
        }

        public List<string> ExcludedColumns
        {
            get { return _excludedColumns; }
            set { _excludedColumns = value; }
        }
        public string CommaSeparatedExcludedColumns
        {
            get { return string.Join(",", ExcludedColumns); }
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