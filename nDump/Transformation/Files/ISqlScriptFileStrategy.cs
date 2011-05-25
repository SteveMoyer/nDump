using System.Collections.Generic;
using nDump.Model;

namespace nDump
{
    public interface ISqlScriptFileStrategy
    {
        IEnumerator<SqlScript> GetEnumeratorFor(string tableName);
    }
}