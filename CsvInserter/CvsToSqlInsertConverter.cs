using System;
using System.Collections.Generic;
using System.Text;

namespace CsvInserter
{
    public class CvsToSqlInsertConverter : ICvsToSqlInsertConverter
    {
        private const string Delimeter = ",";
        private const string NullString = "null";
        private const char QuoteChar = '\'';
        private const string InsertHeaderFormat = "insert {0} ({1})";
        private const string InsertFormat = "{0} values ({1})\n";

        public CvsToSqlInsertConverter()
        {
        }

        public void Convert(CsvTable csvTable)
        {
            csvTable.SqlWriter.Write(GenerateInserts(csvTable));
            csvTable.SqlWriter.Close();

        }
        private  string GenerateInserts(CsvTable csvTable)
        {
            var insertHeader = string.Format(InsertHeaderFormat, csvTable.Name, Join(csvTable.GetColumnNames(), Delimeter));
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
            if (csvTable.HasIdentity)
            {
                builder.AppendFormat("set identity_insert {0} off\n", csvTable.Name);
            }
        }

        private void TurnOnIdentityInsert(CsvTable csvTable, StringBuilder builder)
        {
            if (csvTable.HasIdentity)
            {
                builder.AppendFormat("set identity_insert {0} on\n", csvTable.Name);
            }
        }

        private  bool IsNullEmptyOrNullString(string thisvalue)
        {
            return (string.IsNullOrEmpty(thisvalue) || thisvalue.Equals(NullString, StringComparison.OrdinalIgnoreCase));
        }

        private  string Join(string[] values, string delimeter)
        {
            return Join(values, delimeter, false, ' ');
        }

        private  string Join(string[] values, string delimeter, bool addQuotes, char quoteChar)
        {
            var JoinedValues = "";
            var delimeterIfNotFirstValue = "";
            foreach (var value in values)
            {
                var escapedString = value.Replace("'", "''");
                if (addQuotes)
                {
                    JoinedValues = JoinedValues + delimeterIfNotFirstValue + (IsNullEmptyOrNullString(value) ? NullString : (quoteChar + escapedString + quoteChar));
                }
                else
                {
                    JoinedValues = JoinedValues + delimeterIfNotFirstValue + (string.IsNullOrEmpty(value) ? NullString : escapedString);
                }
                delimeterIfNotFirstValue = delimeter;
            }
            return JoinedValues;
        }

        private  string JoinValues(string[] values)
        {
            return Join(values, Delimeter, true, QuoteChar);
        }



    }
}