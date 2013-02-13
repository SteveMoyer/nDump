/* Copyright 2010-2013 Steve Moyer
 * This file is part of nDump.
 * 
 * nDump is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * nDump is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with nDump.  If not, see <http://www.gnu.org/licenses/>.
*/
﻿using System;
using System.Text;
using nDump.Model;
using nDump.Transformation.Escaping;

namespace nDump.Transformation
{
    public class CsvToSqlInsertConverter : ICsvToSqlInsertConverter
    {
        private readonly int _numberOfRowsPerInsert;
        private readonly IEscapingStrategy _headerEscapingStrategy;
        private readonly IEscapingStrategy _valueEscapingStrategy;
        private const string InsertHeaderFormat = "insert {0} ({1}) values\n";
        private const string InsertFormat = "{0}({1})\n";
        private const string SetIdentityInsertFormatString = "set identity_insert {0} {1}\n";
        private const string Off = "off";
        private const string On = "on";

        public CsvToSqlInsertConverter(IEscapingStrategy headerEscapingStrategy,
                                       IEscapingStrategy valueEscapingStrategy)
        {
            _headerEscapingStrategy = headerEscapingStrategy;
            _valueEscapingStrategy = valueEscapingStrategy;
        }

        public CsvToSqlInsertConverter(int numberOfRowsPerInsert)
            : this(new ColumnHeaderKeywordEscapingStrategy(),
                   new ValueEscapingStrategy())
        {
            _numberOfRowsPerInsert = numberOfRowsPerInsert;
        }

        public void Convert(ICsvTable csvTable)
        {
            GenerateInserts(csvTable);
        }

        private void GenerateInserts(ICsvTable csvTable)
        {
            string insertHeader = string.Format(InsertHeaderFormat, _headerEscapingStrategy.Escape(csvTable.Name),
                                                String.Join(",",
                                                            _headerEscapingStrategy.Escape(csvTable.GetColumnNames())));
            var builder = new StringBuilder();
            StartFile(csvTable, builder, insertHeader);
            int i = 0;

            string separator = string.Empty;
            while (csvTable.ReadNextRow())
            {
                if (i != 0 && i%_numberOfRowsPerInsert == 0)
                {
                    EndFile(csvTable, builder);
                    StartFile(csvTable, builder, insertHeader);
                    separator = string.Empty;
                }
                InsertRow(csvTable, builder, separator);
                separator = ",";
                i++;
            }
            if (i != 0) EndFile(csvTable, builder);
        }

        private void StartFile(ICsvTable csvTable, StringBuilder builder, string insertHeader)
        {
            TurnOnIdentityInsert(csvTable, builder);
            builder.Append(insertHeader);
        }

        private void EndFile(ICsvTable csvTable, StringBuilder builder)
        {
            TurnOffIdentityInsert(csvTable, builder);
            csvTable.Write(builder.ToString());
            builder.Clear();
        }

        private void InsertRow(ICsvTable csvTable, StringBuilder builder, string separator)
        {
            string insertValues = String.Join(",", _valueEscapingStrategy.Escape(csvTable.GetValues()));
            builder.AppendFormat(InsertFormat, separator, insertValues);
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
