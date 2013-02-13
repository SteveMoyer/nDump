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
﻿using Moq;
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
