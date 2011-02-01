namespace CsvInserter
{
    public class CsvFileProcessor : ICsvProcessor
    {
        private readonly string[] _files;
        private readonly ICvsToSqlInsertConverter _cvsToSqlInsertConverter;
        private readonly ICsvTableFactory _csvTableFactory;

        public CsvFileProcessor(string[] files, ICvsToSqlInsertConverter cvsToSqlInsertConverter,
                                ICsvTableFactory csvTableFactory)
        {
            _files = files;
            _cvsToSqlInsertConverter = cvsToSqlInsertConverter;
            _csvTableFactory = csvTableFactory;
        }

        public void Process()
        {
            foreach (var file in _files)
            {
                using (var table = _csvTableFactory.CreateCsvTable(file))
                {
                    _cvsToSqlInsertConverter.Convert(table);
                }
            }
        }
    }
}