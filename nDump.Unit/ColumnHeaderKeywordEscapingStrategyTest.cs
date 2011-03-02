
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
    }
}
