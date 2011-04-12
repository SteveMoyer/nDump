using System.Collections.Generic;

namespace nDump
{
    public interface ISqlScriptFileStrategy
    {
        IEnumerator<SqlScript> GetEnumeratorFor(string tableName);
    }
}