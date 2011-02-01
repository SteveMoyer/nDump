using System;
using System.Text;

namespace CsvInserter
{
    public class CvsToSqlInsertConverter : ICvsToSqlInsertConverter
    {
        private const string SingleSingleQuote = "'";
        private const string DoubleSingleQuote = "''";
        private const string Delimeter = ",";
        private const string NullString = "null";
        private const char QuoteChar = '\'';
        private const string InsertHeaderFormat = "insert {0} ({1})";
        private const string InsertFormat = "{0} values ({1})\n";
        private const string SetIdentityInsertFormatString = "set identity_insert {0} {1}\n";
        private const string Off = "off";
        private const string On = "on";


        public void Convert(CsvTable csvTable)
        {
            csvTable.Write(GenerateInserts(csvTable));
        }

        private string GenerateInserts(CsvTable csvTable)
        {
            string insertHeader = string.Format(InsertHeaderFormat, csvTable.Name,
                                                Join(csvTable.GetColumnNames(), Delimeter));
            var builder = new StringBuilder();

            TurnOnIdentityInsert(csvTable, builder);
            InsertRows(csvTable, builder, insertHeader);
            TurnOffIdentityInsert(csvTable, builder);

            return builder.ToString();
        }

        private void InsertRows(CsvTable csvTable, StringBuilder builder, string insertHeader)
        {
            while (csvTable.ReadNextRow())
            {
                InsertRow(csvTable, builder, insertHeader);
            }
        }

        private void InsertRow(CsvTable csvTable, StringBuilder builder, string insertHeader)
        {
            string insertValues = JoinValues(csvTable.GetValues());
            builder.AppendFormat(InsertFormat, insertHeader, insertValues);
        }

        private void TurnOffIdentityInsert(CsvTable csvTable, StringBuilder builder)
        {
            SetIdentityInsert(csvTable, builder, Off);
        }

        private void SetIdentityInsert(CsvTable csvTable, StringBuilder builder, string value)
        {
            if (csvTable.HasIdentity)
            {
                builder.AppendFormat(SetIdentityInsertFormatString, csvTable.Name, value);
            }
        }

        private void TurnOnIdentityInsert(CsvTable csvTable, StringBuilder builder)
        {
            SetIdentityInsert(csvTable, builder, On);
        }


        private bool IsNullEmptyOrNullString(string thisvalue)
        {
            return (string.IsNullOrEmpty(thisvalue) || thisvalue.Equals(NullString, StringComparison.OrdinalIgnoreCase));
        }

        private string Join(string[] values, string delimeter)
        {
            return Join(values, delimeter, false, ' ');
        }

        private string Join(string[] values, string delimeter, bool addQuotes, char quoteChar)
        {
            var joinedValues = "";
            var delimeterIfNotFirstValue = "";
            foreach (string value in values)
            {
                var escapedString = value.Replace(SingleSingleQuote, DoubleSingleQuote);
                if (addQuotes)
                {
                    joinedValues = joinedValues + delimeterIfNotFirstValue +
                                   (IsNullEmptyOrNullString(value)
                                        ? NullString
                                        : (quoteChar + escapedString + quoteChar));
                }
                else
                {
                    joinedValues = joinedValues + delimeterIfNotFirstValue +
                                   (string.IsNullOrEmpty(value) ? NullString : escapedString);
                }
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