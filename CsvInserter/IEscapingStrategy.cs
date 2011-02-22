﻿namespace CsvInserter
{
    public interface IEscapingStrategy
    {
        string Escape(string value);
        string[] Escape(string[] values);
    }
}