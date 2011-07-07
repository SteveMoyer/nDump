using System.Collections.Generic;
using nDump.Logging;
using nDump.Model;

namespace nDump.Export
{
    public class SqlDataExporter
    {
        
        private readonly ILogger _logger;
        
        private readonly ISelectionFilteringStrategy _selectionFilteringStrategy;
        private readonly CsvGenerator _csvGenerator;

        public SqlDataExporter(ILogger logger, 
                               ISelectionFilteringStrategy selectionFilteringStrategy, CsvGenerator csvGenerator)
        {
            _logger = logger;
            _csvGenerator = csvGenerator;
            _selectionFilteringStrategy = selectionFilteringStrategy;
            
        }

        
        public void ExportToCsv(List<SqlTableSelect> setupScripts, List<SqlTableSelect> selects)
        {
            try
            {
                _selectionFilteringStrategy.TearDownFilterTables(setupScripts);
            }
            catch (TearDownException)
            {
            }
            _selectionFilteringStrategy.SetupFilterTables(setupScripts);
            
            _csvGenerator.Generate(selects);
            _selectionFilteringStrategy.TearDownFilterTables(setupScripts);
        }


    }
}