using nDump.Configuration;

namespace nDump.Console
{
// ReSharper disable InconsistentNaming
    internal class nDumpParser
// ReSharper restore InconsistentNaming
    {
        public nDumpOptions Parse(string[] args)
        {
            var position = 0;
            bool export = false, import = false, transform = false;
            var file = string.Empty;
            var csvDirectory = string.Empty;
            var sqlDirectory = string.Empty;
            var sourceConnection = string.Empty;
            var targetConnection = string.Empty;
            var applyFilters = true;
            var options = string.Empty;
            while (position < args.Length)
            {
                switch (args[position].ToLower())
                {
                    case "-?":
                    case "/?":
                    case "-h":
                    case "/h":
                        throw new nDumpConfigurationException();
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
                    case "-dp":
                        file = args[position + 1];
                        position += 2;
                        break;
                    case "-o":
                        options = args[position + 1];
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
                    case "-nofilter":
                        applyFilters = false;
                        position += 1;
                        break;
                    default:
                        throw new nDumpConfigurationException("Invalid argument supplied, see usage below.\n");
                }
            }
            if (!string.IsNullOrEmpty(options))
            {
                var ndumpOptions = nDumpOptions.Load(options);
                ndumpOptions.File = file;
                return ndumpOptions;
            }
            return new nDumpOptions(export, transform, import, file, csvDirectory, sqlDirectory, sourceConnection,
                                    targetConnection, applyFilters);
        }
    }
}