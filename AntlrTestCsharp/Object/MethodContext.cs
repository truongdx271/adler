using Antlr4.Runtime;
using System.Collections.Generic;

namespace AntlrTestCsharp.Object
{
    public class MethodContext
    {
        public ParserRuleContext context { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public string outputType { get; set; }
        public string queryVar { get; set; }
        public int startLine { get; set; }
        public List<string> inputList { get; set; }
        public List<TempExpression> listExp { get; set; }
        public List<int> lineList { get; set; }

        public MethodContext(ParserRuleContext context, List<int> lineList, int startLine)
        {
            this.context = context;
            this.lineList = lineList;
            this.startLine = startLine;
        }

        public MethodContext() { }
    }
}
