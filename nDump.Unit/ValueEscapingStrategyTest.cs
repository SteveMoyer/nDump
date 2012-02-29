
using nDump.Transformation.Escaping;
using NUnit.Framework;

namespace nDump.Unit
{
    [TestFixture]
    public class ValueEscapingStrategyTest
    {
        private ValueEscapingStrategy _valueEscapingStrategy;

        [SetUp]public void Setup()
        {
            _valueEscapingStrategy = new ValueEscapingStrategy();
        }

        [Test]
        public void ShouldQuoteSingleValue()
        {
            string escape = _valueEscapingStrategy.Escape("test");
            Assert.AreEqual("N'test'",escape);
        }

        [Test]
        public void ShouldReturnNullStringForEmptyString()
        {
            string escape = _valueEscapingStrategy.Escape(string.Empty);
            Assert.AreEqual("null", escape);
        }

        [Test]
        public void ShouldReturnNullStringForNull()
        {
            string escape = _valueEscapingStrategy.Escape((string)null);
            Assert.AreEqual("null", escape);
        }

        [Test]
        public void ShouldReturnNullStringForNullString()
        {
            string escape = _valueEscapingStrategy.Escape("null");
            Assert.AreEqual("null", escape);
        }

        [Test]
        public void ShouldReplaceSingleQuoteWithDoubleSingleQuote()
        {
            string escape = _valueEscapingStrategy.Escape("a'b");
            Assert.AreEqual("N'a''b'", escape);
        }

        [Test]
        public void ShouldEscapeMultipleValues()
        {
            string[] escape = _valueEscapingStrategy.Escape(new string[]{"a'b","b"});
            Assert.AreEqual(new []{"N'a''b'","N'b'"}, escape);
        }
    }
}
