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
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace nDump.Model
{
    [Serializable]
    public class DataPlan
    {
        private List<SqlTableSelect> _setupScripts;
        private List<SqlTableSelect> _dataSelects;

        public DataPlan(List<SqlTableSelect> setupScripts, List<SqlTableSelect> dataSelects)
        {
            _setupScripts = setupScripts;
            _dataSelects = dataSelects;
        }

        public DataPlan(): this(new List<SqlTableSelect>(),new List<SqlTableSelect>() )
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
            set { _setupScripts = value; }
        }

        public void Save(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof (DataPlan));
            using(var textWriter = new FileStream(fileName, FileMode.Create))
                xmlSerializer.Serialize(textWriter, this);
        }

        public static DataPlan Load(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof (DataPlan));
            using(var fileStream = new FileStream(fileName, FileMode.Open,FileAccess.Read))
                return (DataPlan) xmlSerializer.Deserialize(fileStream);
        }
    }
}
