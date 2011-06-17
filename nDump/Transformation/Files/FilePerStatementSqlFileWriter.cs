using System.IO;
using nDump.Logging;

namespace nDump
{
    public class FilePerStatementSqlFileWriter:ISqlFileWriter
    {
        private readonly string _directory;
        private readonly string _fileNameWithoutExtension;
        private readonly ILogger _logger;
        private int _counter = 1;

        public FilePerStatementSqlFileWriter(string directory, string fileNameWithoutExtension, ILogger logger)
        {
            _directory = directory;
            _fileNameWithoutExtension = fileNameWithoutExtension;
            _logger = logger;
        }

        public void Write(string sql)
        {
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
                _logger.Log(_directory + " did not exist: creating\n");
            }
            

            var sw =
                new StreamWriter(_directory + @"\" + _fileNameWithoutExtension + "_" + string.Format("{0:000}", _counter) + ".sql",false);
            _counter++;
            sw.Write(sql);
            sw.Dispose();
        }
    }
}