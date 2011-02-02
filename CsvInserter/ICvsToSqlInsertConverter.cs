namespace CsvInserter
{
    public interface ICvsToSqlInsertConverter
    {
        void Convert(ICsvTable csvTable);
    }
}