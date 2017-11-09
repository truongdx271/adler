using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using AntlrTestCsharp.Object;
using Antlr4.Runtime;
using System.Configuration;
using AntlrTestCsharp.Config;

namespace AntlrTestCsharp.parser
{
    public class FindBadCrypt : CSharpParserBaseListener
    {
        CSharpParser parser;
        public bool isVuln { get; set; }
        List<string> vulnContext;
        public MethodInfor tmpMethod { get; set; }
        public FindBadCrypt(CSharpParser parser)
        {
            this.parser = parser;
            isVuln = true;
            vulnContext = new List<string>();
            //string pathFile = ConfigurationManager.AppSettings["VulnContext"];
            var resourceName = "AntlrTestCsharp.Resources.BadCrypto.txt";
            ConfigLoadItem loadCommand = new ConfigLoadItem(resourceName);
            vulnContext = loadCommand.getListItem();
            //vulnContext.Add(".ProhibitDtd=true");
            //vulnContext.Add(".XmlResolver=null");
            //vulnContext.Add(".DtdProcessing=DtdProcessing.Prohibit");
        }

        public override void EnterExpression([NotNull] CSharpParser.ExpressionContext context)
        {

            foreach (var item in vulnContext)
            {
                if (context.GetText().Contains(item))
                {
                    isVuln = false;
                    //tmpMethod.
                }
            }
            if (tmpMethod == null)
            {
                tmpMethod = new MethodInfor();
                tmpMethod.BaselineItem = 102;
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

            }

            //Console.WriteLine(context.GetText());
        }

        public override void EnterMethod_invocation([NotNull] CSharpParser.Method_invocationContext context)
        {
        }
        public override void EnterAdditive_expression([NotNull] CSharpParser.Additive_expressionContext context)
        {
        
        }

        public override void EnterAssignment([NotNull] CSharpParser.AssignmentContext context)
        {
           
        }
        

    }
}
