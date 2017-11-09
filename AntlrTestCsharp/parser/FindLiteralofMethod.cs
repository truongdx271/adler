using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.parser
{
    public class FindLiteralofMethod : CSharpParserBaseListener
    {
        CSharpParser parser;
        List<string> listVal;
        public bool isConst { get; set; }

        public FindLiteralofMethod(CSharpParser parser, List<string> listVal)
        {
            this.parser = parser;
            this.listVal = listVal;
            this.isConst = false;
        }

        public override void EnterLiteralExpression([NotNull] CSharpParser.LiteralExpressionContext context)
        {
            foreach (var item in listVal)
            {
                if (context.GetText().Equals(item))
                {
                    isConst = true;
                    break;
                }
            }
        }
    }
}
