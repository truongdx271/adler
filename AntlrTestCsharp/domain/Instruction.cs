using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.domain
{
    public class Instruction
    {
        private string name;

        public Instruction(string name)
        {
            this.name = name;
        }

        public string toString()
        {
            return "Instructions{" +
                "\nname='" + name + '\'' +
                '}';
        }
    }
}
