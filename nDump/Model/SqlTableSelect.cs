/* Copyright 2010-2013 Steve Moyer
 * This file is part of nDump.
 * 
 * nDump is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * nDump is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with nDump.  If not, see <http://www.gnu.org/licenses/>.
*/
﻿using System.Collections.Generic;

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
