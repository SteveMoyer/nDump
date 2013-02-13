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
﻿
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
