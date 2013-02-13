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
﻿using System;
using System.Collections.Generic;
using Moq;
using nDump.Import;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;
using nDump.Transformation.Files;
using nDump.Workflow;
using NUnit.Framework;

namespace nDump.Unit
{
    [TestFixture]
    public class SqlDataImporterTest
    {
//        private class IntHolder
//        {
//            
//        }
        [Test]public void ShouldDeleteFromTablesInReverseOrder()
        {
            var queryExecutor= new Mock<IQueryExecutor>();

            var sqlDataImporter = new SqlDataImporter(new NullLogger(), queryExecutor.Object,null);
            
            var calls = new List<string>();
            var table1 = new SqlTableSelect("1");
            var table2=new SqlTableSelect("2");
            var delete2 = "delete from 2";
            queryExecutor.Setup(qe => qe.ExecuteNonQueryStatement(delete2)).Callback((string s)=> calls.Add(s));
            var delete1 = "delete from 1";
            queryExecutor.Setup(qe => qe.ExecuteNonQueryStatement(delete1)).Callback((string s) => calls.Add(s));
            sqlDataImporter.DeleteDataFromAllDestinationTables(new List<SqlTableSelect>{table1,table2});
            Assert.AreEqual(delete1,calls[1]);
            Assert.AreEqual(delete2,calls[0]);

        }
        [Test]
        public void ShouldExecuteEachScriptFoundByStrategy()
        {
            var queryExecutor = new Mock<IQueryExecutor>();
            var scriptStrategy = new Mock<ISqlScriptFileStrategy>();

            var table1 = new SqlTableSelect("1");
            scriptStrategy.Setup(ss => ss.GetEnumeratorFor(table1.TableName)).Returns(new List<SqlScript>
                                                                                          {new SqlScript("01", "01")}.GetEnumerator());
            queryExecutor.Setup(qe => qe.ExecuteNonQueryStatement("01"));
            var sqlDataImporter = new SqlDataImporter(new NullLogger(), queryExecutor.Object, scriptStrategy.Object);

            sqlDataImporter.InsertDataIntoDesinationTables(new List<SqlTableSelect> { table1 });
            queryExecutor.VerifyAll();
            scriptStrategy.VerifyAll();
        }
        [Test]
        public void ShouldNotExecuteEmptyScriptFoundByStrategy()
        {
            var queryExecutor = new Mock<IQueryExecutor>();
            var scriptStrategy = new Mock<ISqlScriptFileStrategy>();

            var table1 = new SqlTableSelect("1");
            scriptStrategy.Setup(ss => ss.GetEnumeratorFor(table1.TableName)).Returns(new List<SqlScript> { new SqlScript("01", "01"), new SqlScript("02", String.Empty) }.GetEnumerator());
            queryExecutor.Setup(qe => qe.ExecuteNonQueryStatement("01")).Verifiable();
            queryExecutor.Setup(qe => qe.ExecuteNonQueryStatement("02")).Throws(new Exception());
            var sqlDataImporter = new SqlDataImporter(new NullLogger(), queryExecutor.Object, scriptStrategy.Object);

            sqlDataImporter.InsertDataIntoDesinationTables(new List<SqlTableSelect> { table1 });
            queryExecutor.Verify();
            scriptStrategy.VerifyAll();
        }
    }
}
