namespace CsvInserter
{
    public interface ICvsToSqlInsertConverter
    {
        void Convert(CsvTable csvTable);
    }
}