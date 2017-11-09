using AntlrTestCsharp.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;

namespace AntlrTestCsharp.parser
{
    public class FindUploadInMethod : CSharpParserBaseListener
    {
        CSharpParser parser;
        //public bool isVuln { get; set; }
        public List<MethodInfor> listMethod { get; set; }
        public FindUploadInMethod(CSharpParser parser)
        {
            this.parser = parser;
            listMethod = new List<MethodInfor>();
        }

        public override void EnterExpression([NotNull] CSharpParser.ExpressionContext context)
        {
            if (context.GetText().Contains(".SaveAs("))
            {
                MethodInfor tmpMethod = new MethodInfor();
                tmpMethod.BaselineItem = 412;
                ParserRuleContext parrentOfTree = (ParserRuleContext)context.Parent;
                while (!(parrentOfTree is CSharpParser.Method_declarationContext))
                {
                    try
                    {
                        parrentOfTree = (ParserRuleContext)parrentOfTree.Parent;
                        if (parrentOfTree == null) { return; }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (parrentOfTree is CSharpParser.Method_declarationContext)
                {
                    tmpMethod.methodName = parrentOfTree.GetChild(0).GetText();
                    tmpMethod.startLine = parrentOfTree.Start.Line;
                }

                if (listMethod != null)
                {
                    if (!listMethod.Any(x => x.startLine == tmpMethod.startLine))
                    {
                        listMethod.Add(tmpMethod);
                    }
                }
                else
                {
                    listMethod.Add(tmpMethod);
                }

            }
        }
    }
}

