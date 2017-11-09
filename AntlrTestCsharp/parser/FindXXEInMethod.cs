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
    public class FindXXEInMethod : CSharpParserBaseListener
    {
        CSharpParser parser;
        public bool isVuln { get; set; }
        List<string> vulnContext;
        public MethodInfor tmpMethod { get; set; }
        public FindXXEInMethod(CSharpParser parser)
        {
            this.parser = parser;
            isVuln = true;
            vulnContext = new List<string>();
            //string pathFile = ConfigurationManager.AppSettings["VulnContext"];
            var resourceName = "AntlrTestCsharp.Resources.VulnContext.txt";
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
                tmpMethod.BaselineItem = 418;
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
            //Tham so truyen vao khi goi method
            //Console.WriteLine(context.GetChild(1).GetText());
            //Console.WriteLine(context.GetChild(1).ChildCount);
        }
        public override void EnterAdditive_expression([NotNull] CSharpParser.Additive_expressionContext context)
        {
            //Expression tach duoc
            //Child: multiplicative_expressionContext
            //Console.WriteLine(context.GetText());
            //if (context.Start.Line != 52)
            //{
            //    return;
            //}
            //for (int i = 0; i < context.ChildCount; i++)
            //{
            //    Console.WriteLine(context.GetChild(i).GetText());
            //}
        }

        public override void EnterAssignment([NotNull] CSharpParser.AssignmentContext context)
        {
            //Gan' gia tri cho bien
            //child: ExpressionContext
            //Console.WriteLine(context.GetChild(2).GetText());
            //Console.WriteLine(context.GetChild(2).GetType().ToString());
            //Console.WriteLine(context.GetChild(0).GetText());
        }

        //public override void EnterObjectCreationExpression([NotNull] CSharpParser.ObjectCreationExpressionContext context)
        //{
        //    Console.WriteLine(context.GetText());
        //    if (context.GetChild(2).ChildCount == 2)
        //    {
        //        ParserRuleContext parentOfTree = (ParserRuleContext)context.Parent;
        //        while (!(parentOfTree is CSharpParser.Local_variable_declaratorContext))
        //        {
        //            try
        //            {
        //                parentOfTree = (ParserRuleContext)parentOfTree.Parent;
        //                if (parentOfTree == null) { return; }
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }
        //        if (parentOfTree is CSharpParser.Local_variable_declaratorContext)
        //        {
        //            Console.WriteLine(parentOfTree.GetChild(0).GetText());
        //        }
        //    }
        //}

    }
}
