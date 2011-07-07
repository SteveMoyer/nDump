using nDump.Export;
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
