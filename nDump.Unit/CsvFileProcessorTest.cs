using Moq;
using nDump.Model;
using nDump.Transformation;
using NUnit.Framework;

namespace nDump.Unit
{
    [TestFixture]
    public class CsvFileProcessorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void ShouldCallConverterForEachFile()
        {
            var converter = new Mock<ICsvToSqlInsertConverter>();
            var tableFactory = new Mock<ICsvTableFactory>();
            var table1 = new Mock<ICsvTable>().Object;
            var table2 = new Mock<ICsvTable>().Object;
            tableFactory.Setup(tf => tf.CreateCsvTable("a")).Returns(table1);
            tableFactory.Setup(tf => tf.CreateCsvTable("b")).Returns(table2);
            converter.Setup(c => c.Convert(table1));
            converter.Setup(c => c.Convert(table2));

            new CsvFileProcessor(new[] {"a", "b"}, converter.Object, tableFactory.Object).Process();

            tableFactory.VerifyAll();
            converter.VerifyAll();
        }
    }
}