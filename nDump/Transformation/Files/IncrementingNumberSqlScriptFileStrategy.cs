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
﻿using System;
using System.Collections.Generic;
using System.IO;
using nDump.Model;

namespace nDump.Transformation.Files
{
    public class IncrementingNumberSqlScriptFileStrategy : ISqlScriptFileStrategy
    {

        private readonly string _scriptDirectory;
        private const string SqlFileNameFormat = "{0}{1}_{2:000}.sql";
       
        public IncrementingNumberSqlScriptFileStrategy(string scriptDirectory)
        {
            
            _scriptDirectory = scriptDirectory;
        }

        public IEnumerator<SqlScript> GetEnumeratorFor(string tableName)
        {
            var i = 1;
            var path = String.Format(SqlFileNameFormat, _scriptDirectory, tableName, i);
            while (File.Exists(path))
            {
                SqlScript script;
                using (var reader = File.OpenText(path))
                {
                    script=  new SqlScript(path, reader.ReadToEnd());
                }
                yield return script;
                i++;
                path = String.Format(SqlFileNameFormat, _scriptDirectory, tableName, i);
            }
        }

    }
}
