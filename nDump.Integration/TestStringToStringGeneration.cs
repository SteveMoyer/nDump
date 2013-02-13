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
using System.IO;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using nDump.Model;
using nDump.Transformation;
using nDump.Transformation.Files;
using NUnit.Framework;

namespace nDump.Integration
{
    [TestFixture]
    public class TestStringToStringGeneration
    {
        private class StringBuildingSqlWriter:ISqlFileWriter
        {
            private readonly StringBuilder _stringBuilder;

            public StringBuildingSqlWriter()
            {
                _stringBuilder = new StringBuilder();
            }

            public void Write(string sql)
            {
                _stringBuilder.Append(sql);
            }
            public new string ToString()
            {
                return _stringBuilder.ToString();
            }
        }
        [Test]
        public void ShouldConvertASingleRow()
        {
            var csvToSqlInsertConverter = new CsvToSqlInsertConverter(5000);
            string csvInput = "col1,col2\n1,2";
            string tableName = "testTable";
            
            var sqlWriter = new StringBuildingSqlWriter();
            
            
            using (var csvTextReader = new CsvReader(new StringReader(csvInput), true))
            {
                csvToSqlInsertConverter.Convert(
                    new CsvTable(csvTextReader,
                                 sqlWriter, tableName, false));
            }

            Assert.AreEqual("insert testTable (col1,col2) values\n(N'1',N'2')\n",sqlWriter.ToString());
        }

        [Test]
        public void ShouldConvertMultipleRows()
        {
            var csvToSqlInsertConverter = new CsvToSqlInsertConverter(5000);
            string csvInput = "col1,col2\n1,2\n3,4";
            string tableName = "testTable";
            var sqlWriter = new StringBuildingSqlWriter();
            
            using (var csvTextReader = new CsvReader(new StringReader(csvInput), true))
            {
                csvToSqlInsertConverter.Convert(new CsvTable(csvTextReader,
                                                             sqlWriter, tableName, false));
            }
            Assert.AreEqual("insert testTable (col1,col2) values\n(N'1',N'2')\n,(N'3',N'4')\n",sqlWriter.ToString());
        }

        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
    }
}
