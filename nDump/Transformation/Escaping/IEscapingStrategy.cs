namespace nDump.Transformation.Escaping
{
    public interface IEscapingStrategy
    {
        string Escape(string value);
        string[] Escape(string[] values);
    }
}