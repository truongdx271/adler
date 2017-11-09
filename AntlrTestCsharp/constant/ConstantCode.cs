using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.constant
{
    public class ConstantCode
    {
        public static int SQL_Injection = 402;

        public static class BaselineCode
        {
            public static int SQL = 402;
            public static int XML = 418;
        }

        public static class ScanType
        {
            public static int SQL = 102;
            public static int XML = 106;
        }
    }
}
