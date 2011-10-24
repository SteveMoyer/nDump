using System.IO;
using System.Text;
using nDump.Logging;

namespace nDump.Transformation.Files
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
            

            var sqlFilePath = _directory + @"\" + _fileNameWithoutExtension + "_" + string.Format("{0:000}", _counter) + ".sql";
            const bool shouldAppend = false;
            var sw = new StreamWriter(sqlFilePath, shouldAppend, Encoding.Unicode);
            _counter++;
            sw.Write(sql);
            sw.Dispose();
        }
    }
}