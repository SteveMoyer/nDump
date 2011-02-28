using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CsvInserter
{
    [Serializable]
    public class DataPlan
    {
        private  List<SqlTableSelect> _setupScripts;
        private  List<SqlTableSelect> _dataSelects;

        public DataPlan(List<SqlTableSelect> setupScripts, List<SqlTableSelect> dataSelects)
        {
            _setupScripts = setupScripts;
            _dataSelects = dataSelects;
        }
        public DataPlan()
        {
        }

        public List<SqlTableSelect> DataSelects
        {
            get { return _dataSelects; }
            set { _dataSelects = value; }
        }

        public List<SqlTableSelect> SetupScripts
        {
            get { return _setupScripts; }
            set { _setupScripts=value; }
        }

        public void Save(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof (DataPlan));
            xmlSerializer.Serialize(new FileStream(fileName, FileMode.Create), this);
        }

        public static DataPlan Load(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof (DataPlan));
            return (DataPlan) xmlSerializer.Deserialize(new FileStream(fileName, FileMode.Open));
        }
    }
}