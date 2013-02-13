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
using System.Linq;

namespace nDump.Transformation.Escaping
{
    public  class ValueEscapingStrategy:IEscapingStrategy 
    {
        private const string SingleSingleQuote = "'";
        private const string DoubleSingleQuote = "''";
        private const string NullString = "null";


        private string EscapeWithQuote(string value)
        {
            return (IsNullEmptyOrNullString(value)
                        ? NullString
                        : ("N" + SingleSingleQuote + value + SingleSingleQuote));
        }


        private bool IsNullEmptyOrNullString(string thisvalue)
        {
            return (string.IsNullOrEmpty(thisvalue) || thisvalue.Equals(NullString, StringComparison.OrdinalIgnoreCase));
        }



        public string Escape(string value)
        {
            var escapedString = value==null? value: value.Replace(SingleSingleQuote, DoubleSingleQuote);
            return EscapeWithQuote(escapedString);
            
        }

        public string[] Escape(string[] values)
        {
            return values.Select(value => Escape(value)).ToArray();
        }
    }
}
