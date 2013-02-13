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
﻿using System;
using nDump.Configuration;
using nDump.Export;
using nDump.Import;
using nDump.Logging;
using nDump.Model;
using nDump.SqlServer;
using nDump.Transformation;
using nDump.Transformation.Files;

namespace nDump.Workflow
{
    public class ConsoleExecutor
    {
        public int ExecuteDataPlan(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            try
            {
                ExportIfSelected(nDumpOptions, logger, dataPlan);
                TransformIfSelected(nDumpOptions, logger, dataPlan);
                ImportIfSelected(nDumpOptions, logger, dataPlan);
                BuklInsertIfSelected(nDumpOptions, logger, dataPlan);
                BulkDeleteIfSelected(nDumpOptions, logger, dataPlan);
                InsertIfSelected(nDumpOptions, logger, dataPlan);
            }
            catch (Exception ex)
            {
                logger.Log(ex);
                return -1;
            }
            return 0;
        }

        private void BuklInsertIfSelected(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (!nDumpOptions.BulkInsert) return;
            try
            {


                var importer = new CsvDataImporter(logger,
                                                   new QueryExecutor(nDumpOptions.TargetConnectionString),
                                                   nDumpOptions.CsvDirectory, nDumpOptions.Delimiter);
                importer.RemoveDataAndImportFromSqlFiles(dataPlan.DataSelects);
            }
            catch (Exception ex)
            {
                throw new nDumpApplicationException("Bulk Import Of Sql Failed.", ex);
            }
        }

        private void InsertIfSelected(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (!nDumpOptions.Insert) return;
            try
            {


                var importer = new CsvDataImporter(logger,
                                                   new QueryExecutor(nDumpOptions.TargetConnectionString),
                                                   nDumpOptions.CsvDirectory, nDumpOptions.Delimiter);
                importer.InsertDataFromSqlFiles(dataPlan.DataSelects);
            }
            catch (Exception ex)
            {
                throw new nDumpApplicationException("Insert Data Of Sql Failed.", ex);
            }
        }

        private void BulkDeleteIfSelected(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (!nDumpOptions.BulkDelete) return;
            try
            {


                var importer = new CsvDataImporter(logger,
                                                   new QueryExecutor(nDumpOptions.TargetConnectionString),
                                                   nDumpOptions.CsvDirectory, nDumpOptions.Delimiter);
                importer.RemoveDataFromSqlFiles(dataPlan.DataSelects);
            }
            catch (Exception ex)
            {
                throw new nDumpApplicationException("Bulk Delete Of Sql Failed.", ex);
            }
        }
        
        private void ImportIfSelected(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (!nDumpOptions.Import) return;
            try
            {
                var importer = new SqlDataImporter(logger,
                                                   new QueryExecutor(nDumpOptions.TargetConnectionString),
                                                   new IncrementingNumberSqlScriptFileStrategy(nDumpOptions.SqlDirectory));
                importer.RemoveDataAndImportFromSqlFiles(dataPlan.DataSelects);
            }
            catch (Exception ex)
            {
                throw new nDumpApplicationException("Import Of Sql Failed.",ex);
            }
        }

      

        private void TransformIfSelected(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (!nDumpOptions.Transform) return;

            try
            {
                var transformer = new DataTransformer(nDumpOptions.SqlDirectory, nDumpOptions.CsvDirectory,
                                                      logger, nDumpOptions.Delimiter);
                transformer.ConvertCsvToSql(dataPlan.DataSelects);
            }
            catch (Exception ex)
            {
                throw new nDumpApplicationException("Export To Csv Failed.", ex);
            }
        }

        private void ExportIfSelected(nDumpOptions nDumpOptions, ILogger logger, DataPlan dataPlan)
        {
            if (!nDumpOptions.Export) return;

            try
            {
                var queryExecutor = new QueryExecutor(nDumpOptions.SourceConnectionString);
                ISelectionFilteringStrategy filteringStrategy =
                    nDumpOptions.ApplyFilters
                        ? (ISelectionFilteringStrategy) new UseFilterIfPresentStrategy(queryExecutor, logger)
                        : new IgnoreFilterStrategy();

                var exporter = new SqlDataExporter(logger, filteringStrategy,
                                                   new CsvGenerator(logger, filteringStrategy, queryExecutor,
                                                                    nDumpOptions.CsvDirectory, nDumpOptions.Delimiter));
                exporter.ExportToCsv(dataPlan.SetupScripts, dataPlan.DataSelects);
            }
            catch (Exception ex)
            {
                throw new nDumpApplicationException("Export To Csv Failed.", ex);
            }
        }
    }
}
