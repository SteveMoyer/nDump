using System.Linq;
using System.Text;

namespace CsvInserter
{
    public class CsvToSqlInsertConverter : ICsvToSqlInsertConverter
    {
        private readonly int _rowsPerChunk;
        private readonly CsvTokenJoiner _csvTokenJoiner;
        private const string InsertHeaderFormat = "insert {0} ({1})";
        private const string InsertFormat = "{0} values ({1})\n";
        private const string SetIdentityInsertFormatString = "set identity_insert {0} {1}\n";
        private const string Off = "off";
        private const string On = "on";

        public CsvToSqlInsertConverter(int rowsPerChunk, CsvTokenJoiner csvTokenJoiner)
        {
            _rowsPerChunk = rowsPerChunk;
            _csvTokenJoiner = csvTokenJoiner;
        }

        public void Convert(ICsvTable csvTable)
        {
            csvTable.Write(GenerateInserts(csvTable));
        }

        private string GenerateInserts(ICsvTable csvTable)
        {
            var builder = new StringBuilder();

            TurnOnIdentityInsert(csvTable, builder);
            InsertRows(csvTable, builder);
            TurnOffIdentityInsert(csvTable, builder);

            return builder.ToString();
        }

        private void InsertRows(ICsvTable csvTable, StringBuilder builder)
        {
            string insertHeader = string.Format(InsertHeaderFormat, csvTable.Name,
                                                _csvTokenJoiner.Join(EscapeColumnNames(csvTable.GetColumnNames())));
            int i = 1;
            while (csvTable.ReadNextRow())
            {
                InsertRow(csvTable, builder, insertHeader);
                if (i%_rowsPerChunk == 0)
                    BreakUpChunkWithGo(builder);
                i++;
            }
        }

        private string[] EscapeColumnNames(string[] columnNames)
        {
            var keyWords = new[] {"user", "group", "database"};
            return columnNames.Select(name => keyWords.Contains(name.ToLower()) ? "[" + name + "]" : name).ToArray();
        }

        private void BreakUpChunkWithGo(StringBuilder builder)
        {
            builder.AppendLine("GO");
        }

        private void InsertRow(ICsvTable csvTable, StringBuilder builder, string insertHeader)
        {
            string insertValues = _csvTokenJoiner.JoinValues(csvTable.GetValues());
            builder.AppendFormat(InsertFormat, insertHeader, insertValues);
        }

        private void TurnOffIdentityInsert(ICsvTable csvTable, StringBuilder builder)
        {
            SetIdentityInsert(csvTable, builder, Off);
        }

        private void SetIdentityInsert(ICsvTable csvTable, StringBuilder builder, string value)
        {
            if (csvTable.HasIdentity)
            {
                builder.AppendFormat(SetIdentityInsertFormatString, csvTable.Name, value);
            }
        }

        private void TurnOnIdentityInsert(ICsvTable csvTable, StringBuilder builder)
        {
            SetIdentityInsert(csvTable, builder, On);
        }


    }
}