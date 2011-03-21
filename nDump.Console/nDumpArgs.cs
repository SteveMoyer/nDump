namespace nDump.Console
{
    internal class nDumpArgs
    {
        private readonly bool _export;
        private readonly bool _transform;
        private readonly bool _import;
        private readonly string _file;
        private readonly string _csvDirectory;
        private readonly string _sqlDiretory;
        private readonly string _sourceConnectionString;
        private string _targetConnectionString;
        private readonly bool _applyFilters;

        public nDumpArgs(bool export, bool transform, bool import, string file, string csvDirectory, string sqlDiretory, string sourceConnectionString, string targetConnectionString, bool applyFilters)
        {
            _export = export;
            _targetConnectionString = targetConnectionString;
            _applyFilters = applyFilters;
            _sourceConnectionString = sourceConnectionString;
            _transform = transform;
            _import = import;
            _file = file;
            _csvDirectory = csvDirectory;
            _sqlDiretory = sqlDiretory;
        }

        public bool ApplyFilters
        {
            get { return _applyFilters; }
        }

        public string SqlDiretory
        {
            get { return _sqlDiretory; }
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