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
﻿namespace nDump.Model
{
    public class SqlScript
    {
        private readonly string _name;
        private readonly string _script;

        public SqlScript(string name, string script)
        {
            _name = name;
            _script = script;
        }

        public string Script
        {
            get { return _script; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
