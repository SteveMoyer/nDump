using System;
using System.Data.SqlClient;

namespace nDump
{
    public class nDumpApplicationException: ApplicationException
    {
        public nDumpApplicationException(string message)
        :base(message){
            
        }

        public nDumpApplicationException(string message, Exception innerException): base(message,innerException)
        {
            
        }
    }
}