using nDump.Model;

namespace nDump.Transformation
{
    public interface ICsvTableFactory
    {
        ICsvTable CreateCsvTable(string file);
    }
}