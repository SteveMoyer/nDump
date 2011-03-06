using System.Text;

namespace nDump
{
    public class CsvToSqlInsertConverter : ICsvToSqlInsertConverter
    {
        private readonly TokenJoiner _tokenJoiner;
        private readonly IEscapingStrategy _headerEscapingStrategy;
        private readonly IEscapingStrategy _valueEscapingStrategy;
        private const string InsertHeaderFormat = "insert {0} ({1})";
        private const string InsertFormat = "{0} values ({1})\n";
        private const string SetIdentityInsertFormatString = "set identity_insert {0} {1}\n";
        private const string Off = "off";
        private const string On = "on";

        public CsvToSqlInsertConverter(TokenJoiner tokenJoiner, IEscapingStrategy headerEscapingStrategy, IEscapingStrategy valueEscapingStrategy)
        {
            _tokenJoiner = tokenJoiner;
            _headerEscapingStrategy = headerEscapingStrategy;
            _valueEscapingStrategy = valueEscapingStrategy;
        }

        public CsvToSqlInsertConverter(int rowsPerChunk)
            : this(new TokenJoiner(), new ColumnHeaderKeywordEscapingStrategy(),
                new ValueEscapingStrategy())
        {
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
            string insertHeader = string.Format(InsertHeaderFormat, _headerEscapingStrategy.Escape(csvTable.Name),
                                                _tokenJoiner.Join(
                                                    _headerEscapingStrategy.Escape(csvTable.GetColumnNames())));

            while (csvTable.ReadNextRow())
            {
                InsertRow(csvTable, builder, insertHeader);
            }
        }

        private void BreakUpChunkWithGo(StringBuilder builder)
        {
            builder.AppendLine("GO");
        }

        private void InsertRow(ICsvTable csvTable, StringBuilder builder, string insertHeader)
        {
            string insertValues = _tokenJoiner.Join(_valueEscapingStrategy.Escape(csvTable.GetValues()));
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