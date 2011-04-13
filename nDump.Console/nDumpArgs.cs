// ReSharper disable CheckNamespace
namespace nDump.Console
// ReSharper restore CheckNamespace
{
    internal class nDumpArgs
    {
        private readonly bool _export;
        private readonly bool _transform;
        private readonly bool _import;
        private readonly string _file;
        private readonly string _csvDirectory;
        private readonly string _sqlDirectory;
        private readonly string _sourceConnectionString;
        private readonly string _targetConnectionString;
        private readonly bool _applyFilters;

        public nDumpArgs(bool export, bool transform, bool import, string file, string csvDirectory, string sqlDirectory,
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
        }

        public string SqlDirectory
        {
            get { return _sqlDirectory; }
        }

        public string CsvDirectory
        {
            get { return _csvDirectory; }
        }

        public string File1
        {
            get { return _file; }
        }

        public bool Import
        {
            get { return _import; }
        }

        public bool Transform
        {
            get { return _transform; }
        }

        public bool Export
        {
            get { return _export; }
        }

        public string SourceConnectionString
        {
            get { return _sourceConnectionString; }
        }

        public string TargetConnectionString
        {
            get { return _targetConnectionString; }
        }
    }
}