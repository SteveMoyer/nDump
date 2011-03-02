namespace nDump
{
    public interface ICsvToSqlInsertConverter
    {
        void Convert(ICsvTable csvTable);
    }
}