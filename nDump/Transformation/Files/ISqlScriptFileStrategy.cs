using System.Collections.Generic;
using nDump.Model;

namespace nDump.Transformation.Files
{
    public interface ISqlScriptFileStrategy
    {
        IEnumerator<SqlScript> GetEnumeratorFor(string tableName);
    }
}