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
﻿using nDump.Export;
using nDump.Model;
using NUnit.Framework;

namespace nDump.Unit
{
public    class IgnoreFilterStrategyTest
    {

    [Test]public void ShouldUseRegularSelectWhenNoFilterPresent()
    {
        ISelectionFilteringStrategy filterStrategy = new IgnoreFilterStrategy();
        Assert.AreEqual("select * from table1",filterStrategy.GetFilteredSelectStatement(new SqlTableSelect("table1")));
    }
    [Test]
    public void ShouldUseRegularSelectWhenFilterPresent()
    {
        ISelectionFilteringStrategy filterStrategy = new IgnoreFilterStrategy();
        Assert.AreEqual("select * from table1", filterStrategy.GetFilteredSelectStatement(new SqlTableSelect("table1","filter",false)));
    }

    }
}
