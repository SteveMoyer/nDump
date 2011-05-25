namespace nDump.Transformation
{
    public class CsvFileProcessor : ICsvProcessor
    {
        private readonly string[] _files;
        private readonly ICsvToSqlInsertConverter _csvToSqlInsertConverter;
        private readonly ICsvTableFactory _csvTableFactory;

        public CsvFileProcessor(string[] files, ICsvToSqlInsertConverter csvToSqlInsertConverter,
                                ICsvTableFactory csvTableFactory)
        {
            _files = files;
            _csvToSqlInsertConverter = csvToSqlInsertConverter;
            _csvTableFactory = csvTableFactory;
        }

        public void Process()
        {
            foreach (var file in _files)
            {
                using (var table = _csvTableFactory.CreateCsvTable(file))
                {
                    _csvToSqlInsertConverter.Convert(table);
                }
            }
        }
    }
}