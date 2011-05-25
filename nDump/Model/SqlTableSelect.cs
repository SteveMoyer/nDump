using System.Collections.Generic;

namespace nDump.Model
{
    public class SqlTableSelect
    {
        public override string ToString()
        {
            return _tableName;
        }

        private  List<string> _excludedColumns = new List<string>();
        private  string _tableName;

        public SqlTableSelect()
        {
        }


        public SqlTableSelect(string tableName,bool deleteOnly = false): this(tableName,string.Empty,false)
        {
            DeleteOnly = deleteOnly;
        }

        public SqlTableSelect(string tableName, string select, bool hasIdentity)
        {
            _tableName = tableName;
            Select = select;
            HasIdentity = hasIdentity;
        }

        public SqlTableSelect(string tableName, string select, bool hasIdentity, List<string> excludedColumns)
            : this(tableName, select, hasIdentity)
        {
            _excludedColumns = excludedColumns;
        }

        public bool DeleteOnly { get; set; }

        public List<string> ExcludedColumns
        {
            get { return _excludedColumns; }
            set { _excludedColumns = value; }
        }
        public string CommaSeparatedExcludedColumns
        {
            get { return string.Join(",", ExcludedColumns); }
        }

        public bool HasIdentity { get; set; }

        public string Select { get; set; }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
    }
}