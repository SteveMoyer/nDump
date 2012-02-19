using System;
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

            Assert.AreEqual("insert testTable (col1,col2) values\n('1','2')\n",sqlWriter.ToString());
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
            Assert.AreEqual("insert testTable (col1,col2) values\n('1','2')\n,('3','4')\n",sqlWriter.ToString());
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