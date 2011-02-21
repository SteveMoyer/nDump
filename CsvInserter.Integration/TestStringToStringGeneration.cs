using System.IO;
using System.Text;
using LumenWorks.Framework.IO.Csv;
using NUnit.Framework;

namespace CsvInserter.Integration
{
    [TestFixture]
    public class TestStringToStringGeneration
    {
        [Test]
        public void ShouldConvertASingleRow()
        {
            var csvToSqlInsertConverter = new CsvToSqlInsertConverter(5000, new CsvTokenJoiner());
            string csvInput = "col1,col2\n1,2";
            var sqlOutput = new StringBuilder();
            string tableName = "testTable";
            using (var csvTextReader = new CsvReader(new StringReader(csvInput), true))
            {
                csvToSqlInsertConverter.Convert(new CsvTable(csvTextReader,
                                                             new StringWriter(sqlOutput), tableName, false));
            }
            Assert.AreEqual("insert testTable (col1,col2) values ('1','2')\n", sqlOutput.ToString());
        }

        [Test]
        public void ShouldConvertMultipleRows()
        {
            var csvToSqlInsertConverter = new CsvToSqlInsertConverter(5000, new CsvTokenJoiner());
            string csvInput = "col1,col2\n1,2\n3,4";
            var sqlOutput = new StringBuilder();
            string tableName = "testTable";
            using (var csvTextReader = new CsvReader(new StringReader(csvInput), true))
            {
                csvToSqlInsertConverter.Convert(new CsvTable(csvTextReader,
                                                             new StringWriter(sqlOutput), tableName, false));
            }
            Assert.AreEqual(
                "insert testTable (col1,col2) values ('1','2')\ninsert testTable (col1,col2) values ('3','4')\n",
                sqlOutput.ToString());
        }

        [Test]
        public void ShouldIssueGoWhenChunkSizeExceeded()
        {
            var csvToSqlInsertConverter = new CsvToSqlInsertConverter(4, new CsvTokenJoiner());
            string csvInput = "col1,col2\n1,2\n3,4\n1,2\n3,4\n1,2\n3,4\n1,2";
            var sqlOutput = new StringBuilder();
            string tableName = "testTable";
            using (var csvTextReader = new CsvReader(new StringReader(csvInput), true))
            {
                csvToSqlInsertConverter.Convert(new CsvTable(csvTextReader,
                                                             new StringWriter(sqlOutput), tableName, false));
            }
            Assert.AreEqual(
                1,
                CountStringOccurrences(sqlOutput.ToString(), "GO"));
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