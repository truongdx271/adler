using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Object
{
    public class TempExpression
    {
        public string value { get; set; }
        public int line { get; set; }
        public bool isVuln { get; set; }

        public TempExpression(string value, int line)
        {
            this.value = value;
            this.line = line;
            this.isVuln = false;
        }
    }
}
