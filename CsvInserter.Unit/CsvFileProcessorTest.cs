using NUnit.Framework;

namespace CsvInserter.Unit
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
            var converter = new Moq.Mock<ICsvToSqlInsertConverter>();
            var tableFactory = new Moq.Mock<ICsvTableFactory>();
            var table1 = new Moq.Mock<ICsvTable>().Object;
            var table2 = new Moq.Mock<ICsvTable>().Object;
            tableFactory.Setup(tf => tf.CreateCsvTable("a")).Returns(table1);
            tableFactory.Setup(tf => tf.CreateCsvTable("b")).Returns(table2);
            converter.Setup(c => c.Convert(table1));
            converter.Setup(c => c.Convert(table2));
            new CsvFileProcessor(new string[] {"a", "b"}, converter.Object, tableFactory.Object).Process();


            tableFactory.VerifyAll();
            converter.VerifyAll();
        }

    }
}