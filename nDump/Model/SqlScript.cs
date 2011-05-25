namespace nDump.Model
{
    public class SqlScript
    {
        private readonly string _name;
        private readonly string _script;

        public SqlScript(string name, string script)
        {
            _name = name;
            _script = script;
        }

        public string Script
        {
            get { return _script; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}