using nDump.Model;

namespace nDump.Transformation
{
    public interface ICsvToSqlInsertConverter
    {
        void Convert(ICsvTable csvTable);
    }
}