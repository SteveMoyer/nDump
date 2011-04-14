// ReSharper disable CheckNamespace
using System;
using System.IO;
using System.Xml.Serialization;

namespace nDump
// ReSharper restore CheckNamespace
{
    public class nDumpOptions
    {
        private bool _export;
        private bool _transform;
        private bool _import;
        private string _file;
        private string _csvDirectory;
        private string _sqlDirectory;
        private string _sourceConnectionString;
        private string _targetConnectionString;
        private bool _applyFilters;

        public nDumpOptions()
        {
        }

        public nDumpOptions(bool export, bool transform, bool import, string file, string csvDirectory, string sqlDirectory,
                         string sourceConnectionString, string targetConnectionString, bool applyFilters)
        {
            _export = export;
            _targetConnectionString = targetConnectionString;
            _applyFilters = applyFilters;
            _sourceConnectionString = sourceConnectionString;
            _transform = transform;
            _import = import;
            _file = file;
            _csvDirectory = csvDirectory;
            _sqlDirectory = sqlDirectory;
        }

        public bool ApplyFilters
        {
            get { return _applyFilters; }
            set { _applyFilters = value; }
        }

        public string SqlDirectory
        {
            get { return _sqlDirectory; }
            set { _sqlDirectory = value; }
        }

        public string CsvDirectory
        {
            get { return _csvDirectory; }
            set { _csvDirectory = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public bool Import
        {
            get { return _import; }
            set { _import = value; }
        }

        public bool Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }

        public bool Export
        {
            get { return _export; }
            set { _export = value; }
        }

        public string SourceConnectionString
        {
            get { return _sourceConnectionString; }
            set { _sourceConnectionString = value; }
        }

        public string TargetConnectionString
        {
            get { return _targetConnectionString; }
            set { _targetConnectionString = value; }
        }

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