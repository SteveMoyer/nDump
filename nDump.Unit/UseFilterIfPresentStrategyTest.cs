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
using System.Diagnostics;
using Moq;
using nDump.Export;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;
using NUnit.Framework;

namespace nDump.Unit
{
    [TestFixture]
    public class UseFilterIfPresentStrategyTest
    {
       [Test]public void    ShouldGetSelectStarWhenFilterNull()
       {
           var filterStrategy = new UseFilterIfPresentStrategy(null, new NullLogger());

           string filteredSelectStatement = filterStrategy.GetFilteredSelectStatement(new SqlTableSelect("t",null,false));
           Assert.AreEqual("select * from t",filteredSelectStatement);
       }
       [Test]
       public void ShouldGetSelectStarWhenFilterEmpty()
       {
           var filterStrategy = new UseFilterIfPresentStrategy(null, new NullLogger());

           string filteredSelectStatement = filterStrategy.GetFilteredSelectStatement(new SqlTableSelect("t",String.Empty,false));
           Assert.AreEqual("select * from t", filteredSelectStatement);
       }
       [Test]
       public void ShouldGetFilterWhenFilterNotNullOrEmpty()
       {
           var filterStrategy = new UseFilterIfPresentStrategy(null, new NullLogger());

           string filteredSelectStatement = filterStrategy.GetFilteredSelectStatement(new SqlTableSelect("t", "x", false));
           Assert.AreEqual("x", filteredSelectStatement);
       }
        [Test] public void ShouldExecuteQueryForEachFilterSelect()
        {
            var queryExecutor = new Mock<IQueryExecutor>();

            var filterStrategy = new UseFilterIfPresentStrategy(queryExecutor.Object, new NullLogger());
            var select1=new SqlTableSelect(null,"1",false);
            var select2 = new SqlTableSelect(null, "2", false);
            queryExecutor.Setup(q => q.ExecuteNonQueryStatement("1"));
            queryExecutor.Setup(q => q.ExecuteNonQueryStatement("2"));
            filterStrategy.SetupFilterTables(new List<SqlTableSelect>{select1,select2}); 
            queryExecutor.VerifyAll();

        }
        [Test]
        public void ShouldExecuteDeleteQueryForEachFilterSelect()
        {
            var queryExecutor = new Mock<IQueryExecutor>();

            var filterStrategy = new UseFilterIfPresentStrategy(queryExecutor.Object, new NullLogger());
            var select1 = new SqlTableSelect("1", null, false);
            var select2 = new SqlTableSelect( "2", null, false);
            queryExecutor.Setup(q => q.ExecuteNonQueryStatement("drop table 1"));
            queryExecutor.Setup(q => q.ExecuteNonQueryStatement("drop table 2"));
            filterStrategy.TearDownFilterTables(new List<SqlTableSelect> { select1, select2 });
            queryExecutor.VerifyAll();

        }
        [Test]
        public void ShouldContinueThenThrowTearDownExceptionWhenDeleteFails()
        {
            var queryExecutor = new Mock<IQueryExecutor>();

            var filterStrategy = new UseFilterIfPresentStrategy(queryExecutor.Object, new NullLogger());
            var select1 = new SqlTableSelect("1", null, false);
            var select2 = new SqlTableSelect("2", null, false);
            queryExecutor.Setup(q => q.ExecuteNonQueryStatement("drop table 1")).Throws(new Exception());
            queryExecutor.Setup(q => q.ExecuteNonQueryStatement("drop table 2"));
            try
            {
                filterStrategy.TearDownFilterTables(new List<SqlTableSelect> { select1, select2 });
                throw new AssertionException("ExpectedException was not thrown");
            }
            catch (TearDownException)
            {
                queryExecutor.VerifyAll();                
                
            }


        }
    }
}
