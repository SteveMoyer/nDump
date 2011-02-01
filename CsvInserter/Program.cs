using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LumenWorks.Framework.IO.Csv;

namespace CsvInserter
{
    public class Program
    {
        private static string GenerateInserts(string tablename, CsvReader reader, bool hasIdentity)
        {
            var str = string.Format("insert {0} ({1})", tablename, Join(reader.GetFieldHeaders(), ","));
            var builder = new StringBuilder();
            if (hasIdentity)
            {
                builder.AppendFormat("set identity_insert {0} on\n", tablename);
            }
            while (reader.ReadNextRecord())
            {
                string str2 = JoinValues(reader);
                builder.AppendFormat("{0} values ({1})\n", str, str2);
            }
            if (hasIdentity)
            {
                builder.AppendFormat("set identity_insert {0} off\n", tablename);
            }
            return builder.ToString();
        }

        private static bool IsNullOrEmptyOrNullString(string thisvalue)
        {
            return (string.IsNullOrEmpty(thisvalue) || thisvalue.Equals("null", StringComparison.OrdinalIgnoreCase));
        }

        private static string Join(string[] values, string delimeter)
        {
            return Join(values, delimeter, false, ' ');
        }

        private static string Join(string[] values, string delimeter, bool addQuotes, char quoteChar)
        {
            var str = "";
            var str2 = "";
            foreach (var str3 in values)
            {
                var str4 = str3.Replace("'", "''");
                if (addQuotes)
                {
                    str = str + str2 + (IsNullOrEmptyOrNullString(str3) ? "null" : (quoteChar + str4 + quoteChar));
                }
                else
                {
                    str = str + str2 + (string.IsNullOrEmpty(str3) ? "null" : str4);
                }
                str2 = delimeter;
            }
            return str;
        }

        private static string JoinValues(CsvReader reader)
        {
            var fieldCount = reader.FieldCount;
            var values = new string[fieldCount];
            for (var i = 0; i < fieldCount; i++)
            {
                values[i] = reader[i];
            }
            return Join(values, ",", true, '\'');
        }

        public static void Main(string[] args)
        {
            var path = args[0];
            var outputPath = args[1];
            IList<string> tablesWithoutIdentities = new List<string>(args[2].Split(new[] {','}));
            var files = Directory.GetFiles(path);
//            ICvsToSqlInsertConverter cvsToSqlInsertConverter = new CvsToSqlInsertConverter();
//            ICsvProcessor csvFileProcessor = new CsvFileProcessor(files,cvsToSqlInsertConverter, new CsvTableFactory(outputPath,tablesWithoutIdentities));
//            csvFileProcessor.Process();
            foreach (var fileName in files)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                var writer = new StreamWriter(File.OpenWrite(outputPath + @"\" + fileNameWithoutExtension + ".sql"));
                var reader = new CsvReader(File.OpenText(fileName), true, ',', '\'', '\'', '#',
                                           ValueTrimmingOptions.UnquotedOnly);
                var hasIdentity = !tablesWithoutIdentities.Contains(fileNameWithoutExtension);
                writer.Write(GenerateInserts(fileNameWithoutExtension, reader, hasIdentity));
                writer.Close();
            }
        }
    }
}