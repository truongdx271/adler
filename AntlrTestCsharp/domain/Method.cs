using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.domain
{
    public class Method
    {
        private string name;
        private List<Instruction> instructions;

        public Method(string name, List<Instruction> instructions)
        {
            this.name = name;
            this.instructions = instructions;
        }

        public string toString()
        {
            return "Method{" +
                "\nname='" + name + '\'' +
                "\ninstructions=" + instructions +
                '}';
        }
    }
}
