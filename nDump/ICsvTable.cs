using System;

namespace nDump
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
}