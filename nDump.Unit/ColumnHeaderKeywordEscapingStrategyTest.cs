
using nDump.Transformation.Escaping;
using NUnit.Framework;

namespace nDump.Unit
{
    [TestFixture]
    public class ColumnHeaderKeywordEscapingStrategyTest
    {

        [Test]
        public void ShouldEscapeLowerCaseGroup()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped =g.Escape("group");

            Assert.AreEqual("[group]",escaped);
        }
        [Test]
        public void ShouldEscapeMixedCaseGroup()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("Group");

            Assert.AreEqual("[Group]", escaped);
        }
        [Test]
        public void ShouldEscapeUser()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("user");

            Assert.AreEqual("[user]", escaped);
        }
        [Test]
        public void ShouldEscapeDatabase()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("database");

            Assert.AreEqual("[database]", escaped);
        }
        [Test]
        public void ShouldNotEscapeOther()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("other");

            Assert.AreEqual("other", escaped);
        }
        [Test]
        public void ShouldEscapeOnlyKeyword()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape(new string[] {"other","database"});

            Assert.AreEqual(new string[]{"other","[database]"}, escaped);
        }
        [Test]
        public void ShouldEscapeASpace()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("What happened");

            Assert.AreEqual("[What happened]", escaped);
        }
        [Test]
        public void ShouldEscapeQuestionMarks()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("When?");

            Assert.AreEqual("[When?]", escaped);
        }
        [Test]
        public void ShouldEscapeSlashes()
        {
            var g = new ColumnHeaderKeywordEscapingStrategy();
            var escaped = g.Escape("Today/Tomorrow");

            Assert.AreEqual("[Today/Tomorrow]", escaped);
        }
    }
}
