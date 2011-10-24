using System;
using System.Collections.Generic;
using System.IO;
using nDump.Model;

namespace nDump.Transformation.Files
{
    public class IncrementingNumberSqlScriptFileStrategy : ISqlScriptFileStrategy
    {

        private readonly string _scriptDirectory;
        private const string SqlFileNameFormat = "{0}{1}_{2:000}.sql";
       
        public IncrementingNumberSqlScriptFileStrategy(string scriptDirectory)
        {
            
            _scriptDirectory = scriptDirectory;
        }

        public IEnumerator<SqlScript> GetEnumeratorFor(string tableName)
        {
            var i = 1;
            var path = String.Format(SqlFileNameFormat, _scriptDirectory, tableName, i);
            while (File.Exists(path))
            {
                SqlScript script;
                using (var reader = File.OpenText(path))
                {
                    script=  new SqlScript(path, reader.ReadToEnd());
                }
                yield return script;
                i++;
                path = String.Format(SqlFileNameFormat, _scriptDirectory, tableName, i);
            }
        }

    }
}