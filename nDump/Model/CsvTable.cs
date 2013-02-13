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
﻿using LumenWorks.Framework.IO.Csv;
using nDump.Transformation.Files;

namespace nDump.Model
{
    public class CsvTable : ICsvTable
    {
        private readonly CsvReader _csvTextReader;
        private readonly ISqlFileWriter _sqlFileWriter;
        private readonly string _name;
        private readonly bool _hasIdentity;

        public CsvTable(CsvReader csvTextReader, ISqlFileWriter sqlFileWriter, string name, bool hasIdentity)
        {
            _csvTextReader = csvTextReader;
            _sqlFileWriter = sqlFileWriter;
            _name = name;
            _hasIdentity = hasIdentity;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool HasIdentity
        {
            get { return _hasIdentity; }
        }


        public string[] GetColumnNames()
        {
            return _csvTextReader.GetFieldHeaders();
        }

        public bool ReadNextRow()
        {
            return _csvTextReader.ReadNextRecord();
        }

        public string[] GetValues()
        {
            var fieldCount = _csvTextReader.FieldCount;
            var values = new string[fieldCount];
            for (var i = 0; i < fieldCount; i++)
            {
                values[i] = _csvTextReader[i];
            }
            return values;
        }

        public void Dispose()
        {
            _csvTextReader.Dispose();
        }

        public void Write(string outputString)
        {
            _sqlFileWriter.Write(outputString);
        }
    }
}
