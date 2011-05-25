using nDump.Configuration;
using nDump.Export;
using nDump.Import;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;
using nDump.Transformation;

namespace nDump.Workflow
{
    public class ConsoleExecutor
    {
        public int ExecuteDataPlan(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (nDumpOptions.Export)
            {
                var queryExecutor = new QueryExecutor(nDumpOptions.SourceConnectionString);
                ISelectionFilteringStrategy filteringStrategy = nDumpOptions.ApplyFilters ? (ISelectionFilteringStrategy)
                                                                                         new UseFilterIfPresentStrategy(queryExecutor, logger)
                                                                    : new IgnoreFilterStrategy();
                var exporter = new SqlDataExporter(logger, nDumpOptions.CsvDirectory,
                                                   queryExecutor, filteringStrategy);
                try
                {
                    exporter.ExportToCsv(dataPlan.SetupScripts, dataPlan.DataSelects);

                }
                catch (nDumpApplicationException ex)
                {

                    logger.Log("Export To Csv Failed.\n" + ex.StackTrace);
                    return -1;

                }
            }
            if (nDumpOptions.Transform)
            {
                var transformer = new DataTransformer(nDumpOptions.SqlDirectory, nDumpOptions.CsvDirectory,
                                                      logger);
                try
                {

                    transformer.ConvertCsvToSql(dataPlan.DataSelects);

                }
                catch (nDumpApplicationException ex)
                {

                    logger.Log("Export To Csv Failed.\n" + ex.StackTrace);
                    return -1;

                }
            }
            if (nDumpOptions.Import)
            {
                var importer = new SqlDataImporter(logger,
                                                   new QueryExecutor(nDumpOptions.TargetConnectionString), new IncrementingNumberSqlScriptFileStrategy(nDumpOptions.SqlDirectory));
                try
                {
                    importer.RemoveDataAndImportFromSqlFiles(dataPlan.DataSelects);
                }
                catch (nDumpApplicationException ex)
                {

                    logger.Log("Import Of Sql Failed.\n" + ex.Message + "\n" + ex.StackTrace);
                    return -1;

                }

            }
            return 0;
        }
    }
}