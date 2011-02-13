using System;
using System.Text;

namespace CsvInserter
{
    public class CsvToSqlInsertConverter : ICsvToSqlInsertConverter
    {
        private readonly int _rowsPerChunk;
        private const string Delimeter = ",";
        private const char QuoteChar = '\'';
        private const string InsertHeaderFormat = "insert {0} ({1})";
        private const string InsertFormat = "{0} values ({1})\n";
        private const string SetIdentityInsertFormatString = "set identity_insert {0} {1}\n";
        private const string Off = "off";
        private const string On = "on";

        public CsvToSqlInsertConverter(int rowsPerChunk)
        {
            _rowsPerChunk = rowsPerChunk;
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
                                    Join(csvTable.GetColumnNames(), Delimeter));
            int i = 1;
            while (csvTable.ReadNextRow())
            {
                InsertRow(csvTable, builder, insertHeader);
                if (i % _rowsPerChunk == 0)
                    BreakUpChunkWithGo(builder);
                i++;
            }
        }

        private void BreakUpChunkWithGo(StringBuilder builder)
        {
            builder.AppendLine("GO");
        }

        private void InsertRow(ICsvTable csvTable, StringBuilder builder, string insertHeader)
        {
            string insertValues = JoinValues(csvTable.GetValues());
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


        private string Join(string[] values, string delimeter)
        {
            return Join(values, delimeter, false, ' ');
        }

        private string Join(string[] values, string delimeter, bool addQuotes, char quoteChar)
        {
            var joinedValues = "";
            var delimeterIfNotFirstValue = "";
            var escapingStrategy = new EscapingStrategy( quoteChar);
            foreach (string value in values)
            {
                joinedValues= joinedValues+ delimeterIfNotFirstValue + escapingStrategy.Escape(value, addQuotes);
                delimeterIfNotFirstValue = delimeter;
            }
            return joinedValues;
        }

        private string JoinValues(string[] values)
        {
            return Join(values, Delimeter, true, QuoteChar);
        }
    }
}