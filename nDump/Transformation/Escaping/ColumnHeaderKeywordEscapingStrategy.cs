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
﻿using System.Linq;

namespace nDump.Transformation.Escaping
{
    public class ColumnHeaderKeywordEscapingStrategy : IEscapingStrategy
    {
        private readonly string[] _keyWords = new[] {"user", "group", "database"};
        private readonly string[] _nonsense = new[] {" ", "?", "/"};

        public string Escape(string value)
        {
            var hasNonsense = _nonsense.Any(x => value.Contains(x));
            var isKeyword = _keyWords.Contains(value.ToLower());
            return (isKeyword || hasNonsense) ? "[" + value + "]" : value;
        }

        public string[] Escape(string[] values)
        {
            return values.Select(name => Escape(name)).ToArray();
        }
    }
}
