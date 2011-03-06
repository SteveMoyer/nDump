namespace nDump.Console
{
    internal class nDumpParser
    {
        public nDumpArgs Parse(string[] args)
        {
            int position = 0;
            bool export = false, import = false, transform = false;
            string file = string.Empty;
            string csvDirectory = string.Empty;
            string sqlDirectory = string.Empty;
            string sourceConnection = string.Empty;
            string targetConnection = string.Empty;
            while (position < args.Length)
            {
                switch (args[position].ToLower())
                {
                    case "-e":
                        export = true;
                        position++;
                        break;
                    case "-t":
                        transform = true;
                        position++;
                        break;
                    case "-i":
                        import = true;
                        position++;
                        break;
                    case "-f":
                        file = args[position + 1];
                        position += 2;
                        break;
                    case "-csv":
                        csvDirectory = args[position + 1];
                        position += 2;
                        break;
                    case "-sql":
                        sqlDirectory = args[position + 1];
                        position += 2;
                        break;
                    case "-sourceconnection":
                        sourceConnection = args[position + 1];
                        position += 2;
                        break;
                    case "-targetconnection":
                        targetConnection = args[position + 1];
                        position += 2;
                        break;
                }
            }
            return new nDumpArgs(export, transform, import, file, csvDirectory, sqlDirectory, sourceConnection,
                                 targetConnection);
        }
    }
}