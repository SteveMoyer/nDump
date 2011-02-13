namespace CsvInserter
{
    public interface ICsvToSqlInsertConverter
    {
        void Convert(ICsvTable csvTable);
    }
}