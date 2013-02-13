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
﻿using System.IO;
using System.Xml.Serialization;

namespace nDump.Configuration
{
// ReSharper disable InconsistentNaming
    public class nDumpOptions
// ReSharper restore InconsistentNaming
    {
        public nDumpOptions()
        {
        }

        public nDumpOptions(bool export, bool transform, bool import, string file, string csvDirectory, string sqlDirectory, string sourceConnectionString, string targetConnectionString, bool applyFilters, bool bulkInsert, bool bulkDelete, bool insert)
        {
            Export = export;
            TargetConnectionString = targetConnectionString;
            ApplyFilters = applyFilters;
            BulkInsert = bulkInsert;
            BulkDelete = bulkDelete;
            Insert = insert;
            SourceConnectionString = sourceConnectionString;
            Transform = transform;
            Import = import;
            File = file;
            CsvDirectory = csvDirectory;
            SqlDirectory = sqlDirectory;
            Delimiter = ',';
        }

        public bool ApplyFilters { get; set; }

        public bool BulkInsert{ get;set;}

        public bool Insert { get; set; }

        public bool BulkDelete { get; set; }
        public string SqlDirectory { get; set; }

  		public string BulkInsertSqlDirectory
        {
            get{return string.Format(@"{0}bulkInsert\", CsvDirectory);}
        }
        public string CsvDirectory { get; set; }

        public string File { get; set; }

        public bool Import { get; set; }

        public bool Transform { get; set; }

        public bool Export { get; set; }

        public string SourceConnectionString { get; set; }

        public string TargetConnectionString { get; set; }

        public char Delimiter { get; set; }

        public void Save(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(nDumpOptions));
            using (var textWriter = new FileStream(fileName, FileMode.Create))
                xmlSerializer.Serialize(textWriter, this);
        }

        public static nDumpOptions Load(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(nDumpOptions));
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                return (nDumpOptions)xmlSerializer.Deserialize(fileStream);
        }
    }
}
