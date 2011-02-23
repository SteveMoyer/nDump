namespace CsvInserter
{
    public interface ICsvTableFactory
    {
        ICsvTable CreateCsvTable(string file);
    }
}