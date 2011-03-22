using System.IO;

namespace nDump
{
    public class FilePerStatementSqlFileWriter:ISqlFileWriter
    {
        private readonly string _directory;
        private readonly string _fileNameWithoutExtension;
        private int _counter = 1;

        public FilePerStatementSqlFileWriter(string directory, string fileNameWithoutExtension)
        {
            _directory = directory;
            _fileNameWithoutExtension = fileNameWithoutExtension;
        }

        public void Write(string sql)
        {
            var sw = new StreamWriter(_directory + @"\" + _fileNameWithoutExtension + "_" + string.Format("{0:000}", _counter) + ".sql", false);
            _counter++;
            sw.Write(sql);
            sw.Dispose();
        }
    }
}