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
