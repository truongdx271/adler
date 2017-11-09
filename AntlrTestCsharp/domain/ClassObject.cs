using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AntlrTestCsharp.domain
{
    public class ClassObject
    {
        private string name;
        private List<Method> methods;

        public ClassObject(string name, List<Method> methods)
        {
            this.name = name;
            this.methods = methods;
        }

        public string toString()
        {
            return "Class{" +
                "\nname='" + name + '\'' +
                "\nmethods=" + methods +
                '}';
        }
    }
}
