using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Object
{
    public class MethodInfor
    {
        public string packageName { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public List<string> listArgs { get; set; }
        public List<TempExpression> listExp { get; set; }
        public string outputType { get; set; }
        public int[] injectableArgs { get; set; }
        public int scanType { get; set; }
        public int startLine { get; set; }
        public int BaselineItem { get; set; }

        public MethodInfor(string packageName, string className, string methodName, string outputType, int[] injectableArgs, int scanType, int startLine)
        {
            this.packageName = packageName;
            this.className = className;
            this.methodName = methodName;
            this.outputType = outputType;
            this.injectableArgs = injectableArgs;
            this.scanType = scanType;
            this.startLine = startLine;
        }
        public MethodInfor() { }
    }
}
