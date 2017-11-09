using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntlrTestCsharp.Object
{
    public class VariableDefine
    {
        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }

        public VariableDefine(string type, string name)
        {
            this.type = type;
            this.name = name;
        }
        public VariableDefine(string type, string name, string value)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }
    }
}
