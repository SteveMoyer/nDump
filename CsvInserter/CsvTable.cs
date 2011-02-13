using System;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace CsvInserter
{
    public interface ICsvTable : IDisposable
    {
        string Name { get; }
        bool HasIdentity { get; }
        string[] GetColumnNames();
        bool ReadNextRow();
        string[] GetValues();
        void Write(string outputString);
    }

    public class CsvTable :  ICsvTable
    {
        private readonly CsvReader _csvTextReader;
        private readonly TextWriter _sqlWriter;
        private readonly string _name;
        private readonly bool _hasIdentity;

        public CsvTable(CsvReader csvTextReader, TextWriter sqlWriter, string name, bool hasIdentity)
        {
            _csvTextReader = csvTextReader;
            _sqlWriter = sqlWriter;
            _name = name;
            _hasIdentity = hasIdentity;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool HasIdentity
        {
            get { return _hasIdentity; }
        }


        public string[] GetColumnNames()
        {
            return _csvTextReader.GetFieldHeaders();
        }

        public bool ReadNextRow()
        {
            return _csvTextReader.ReadNextRecord();
        }

        public string[] GetValues()
        {
            var fieldCount = _csvTextReader.FieldCount;
            var values = new string[fieldCount];
            for (var i = 0; i < fieldCount; i++)
            {
                values[i] = _csvTextReader[i];
            }
            return values;
        }

        public void Dispose()
        {
            _sqlWriter.Close();
            _csvTextReader.Dispose();
        }

        public void Write(string outputString)
        {
            _sqlWriter.Write(outputString);
        }
    }
}