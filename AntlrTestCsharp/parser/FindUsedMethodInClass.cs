using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using AntlrTestCsharp.Object;
using AntlrTestCsharp.Config;
using Antlr4.Runtime;
using AntlrTestCsharp.Tracer;
using System.Configuration;

namespace AntlrTestCsharp.parser
{
    public class FindUsedMethodInClass : CSharpParserBaseListener
    {
        CSharpParser parser;
        List<ExpressionCase> listCase;
        List<TempExpression> tmpExpression;
        public MethodContext method { get; set; }
        public List<MethodInfor> listResult { get; set; }
        public FindUsedMethodInClass(CSharpParser parser, MethodContext method)
        {
            this.parser = parser;
            this.method = method;
            //string filePath = @"C:\Users\admin\Desktop\SpecialCase.txt";
            //string filePath = ConfigurationManager.AppSettings["SpecialCase"];
            var resourceName = "AntlrTestCsharp.Resources.SpecialCase.txt";
            listCase = new List<ExpressionCase>();
            ConfigLoadCase config = new ConfigLoadCase(resourceName);
            listCase = config.getListCase();
            sortList(this.method.lineList, 0, this.method.lineList.Count - 1);
        }

        public override void EnterExpression([NotNull] CSharpParser.ExpressionContext context)
        {
            //foreach(var item in method.lineList)
            //{
            //    if (context.Start.Line != item)
            //    {
            //        return;
            //    }
            //}

            if (!method.lineList.Contains(context.Start.Line))
            {
                return;
            }

            foreach (var item in listCase)
            {
                tmpExpression = new List<TempExpression>();
                if (context.GetText().Contains(item.signal))
                {
                    string[] part = context.GetText().Split(char.Parse(item.partition));
                    for (int i = 0; i < part.Length; i++)
                    {
                        //Console.WriteLine(part[i].Replace(")", ""));
                        tmpExpression.Add(new TempExpression(part[i].Replace(")", ""), context.Start.Line));
                    }
                    if (tmpExpression.Count == 0)
                    {
                        continue;
                    }
                    ParserRuleContext parentOfTree = (ParserRuleContext)context.Parent;
                    while (!(parentOfTree is CSharpParser.Method_declarationContext))
                    {
                        try
                        {
                            parentOfTree = (ParserRuleContext)parentOfTree.Parent;
                            if (parentOfTree == null) { return; }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (parentOfTree is CSharpParser.Method_declarationContext)
                    {
                        TraceForCase102 tracer = new TraceForCase102(parser, parentOfTree, tmpExpression);
                        if (!tracer.isVulnMethod)
                        {
                            continue;
                        }
                        if (listResult != null)
                        {
                            if (!(listResult.Any(x => x.methodName == tracer.methodInfor.methodName)))
                            {
                                listResult.Add(tracer.methodInfor);
                            }
                            else
                            {
                                if (!(listResult.SingleOrDefault(x => x.methodName == tracer.methodInfor.methodName).listExp.Any(x => x.line == tracer.methodInfor.listExp[0].line)))
                                {
                                    MethodInfor tmpMethod = listResult.SingleOrDefault(x => x.methodName == tracer.methodInfor.methodName);
                                    foreach (var tmpItem in tracer.methodInfor.listExp)
                                    {
                                        tmpMethod.listExp.Add(tmpItem);
                                    }
                                    listResult.RemoveAll(x => x.methodName == tracer.methodInfor.methodName);
                                    listResult.Add(tmpMethod);
                                }
                            }
                        }
                        else
                        {
                            listResult = new List<MethodInfor>();
                            listResult.Add(tracer.methodInfor);
                        }
                    }

                    //printResult(tracer.methodInfor);

                }
            }




        }

        private void printResult(MethodInfor methodInfor)
        {
            throw new NotImplementedException();
        }

        private void sortList(List<int> input, int left, int right)
        {
            int i = left, j = right;
            int pivot = input[(left + right) / 2];

            while (i <= j)
            {
                while (input[i] < pivot) { i++; }
                while (input[j] > pivot) { j--; }
                if (i <= j)
                {
                    int tmp = input[i];
                    input[i] = input[j];
                    input[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (left < j)
            {
                sortList(input, left, j);
            }

            if (i < right)
            {
                sortList(input, i, right);
            }
        }




        public override void EnterArg_declaration([NotNull] CSharpParser.Arg_declarationContext context)
        {
            //input method
            //Console.WriteLine(context.ChildCount);
            //Console.WriteLine(context.GetText());
        }

        //public override void EnterExpression_list([NotNull] CSharpParser.Expression_listContext context)
        //{
        //    Console.WriteLine(context.GetText());
        //}

        public override void EnterLiteralExpression([NotNull] CSharpParser.LiteralExpressionContext context)
        {
            //nhung doan la string trong dau " "
            // Console.WriteLine(context.GetText());
        }

        public override void EnterLiteral([NotNull] CSharpParser.LiteralContext context)
        {
            //Console.WriteLine(context.GetText());
        }
        public override void EnterLiteralAccessExpression([NotNull] CSharpParser.LiteralAccessExpressionContext context)
        {
            //Console.WriteLine(context.GetText());
        }
        public override void EnterQuery_expression([NotNull] CSharpParser.Query_expressionContext context)
        {
            //context.GetText();
        }
        public override void EnterWhere_clause([NotNull] CSharpParser.Where_clauseContext context)
        {
            //Console.WriteLine(context.GetText());
        }
        public override void EnterPrimary_expression([NotNull] CSharpParser.Primary_expressionContext context)
        {
            //if (context.ChildCount == 3) { 
            //Console.WriteLine(context.GetChild(2).GetType().ToString());
            //}
        }
        public override void EnterMethod_invocation([NotNull] CSharpParser.Method_invocationContext context)
        {
            //Console.WriteLine(context.GetChild(1).GetType().ToString());
        }
        public override void EnterArgument_list([NotNull] CSharpParser.Argument_listContext context)
        {
            //if (context.ChildCount == 7) {
            //    for (int i = 0; i < context.ChildCount; i++)
            //    {
            //        Console.WriteLine(context.GetChild(i).GetType().ToString());
            //    }
            //}
        }
    }
}
