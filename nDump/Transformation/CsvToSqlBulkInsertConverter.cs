using System.IO;
using System.Linq;
using nDump.Logging;
using nDump.Model;
using nDump.Transformation.Escaping;

namespace nDump.Transformation {
    public class CsvToSqlBulkInsertConverter : ICsvToSqlInsertConverter {
        private readonly ILogger logger;
        private readonly IEscapingStrategy _headerEscapingStrategy;

        private const string INSERT_HEADER_FORMAT = "BULK INSERT {0} FROM '{1}' WITH " +
                                                  "(FIELDTERMINATOR = ',', ROWTERMINATOR = '\\n', FIRSTROW=2, ORDER ( {2} ))";

        public CsvToSqlBulkInsertConverter(IEscapingStrategy headerEscapingStrategy) {
            _headerEscapingStrategy = headerEscapingStrategy;
        }

        public CsvToSqlBulkInsertConverter(ILogger logger)
            : this(new ColumnHeaderKeywordEscapingStrategy()){
            this.logger = logger;
        }

        public void Convert(ICsvTable csvTable, string file){
            GenerateInserts(csvTable, file);
        }

        private void GenerateInserts(ICsvTable csvTable, string file){
            string columnNames = null;

            using (var fp = File.OpenRead(file)){
                var data = new StreamReader(fp).ReadLine();
                columnNames = (data ?? string.Empty)
                    .Split(',')
                    .Select(x => string.Format("[{0}]", x))
                    .Aggregate((x, y) => string.Format("{0},{1}", x, y));
            }

            csvTable.Write(string.Format(INSERT_HEADER_FORMAT, _headerEscapingStrategy.Escape(csvTable.Name), file, columnNames));
        }
    }
}