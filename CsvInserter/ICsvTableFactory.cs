namespace nDump
{
    public interface ICsvTableFactory
    {
        ICsvTable CreateCsvTable(string file);
    }
}