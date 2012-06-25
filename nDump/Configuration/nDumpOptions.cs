using System.IO;
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