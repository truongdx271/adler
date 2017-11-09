using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using AntlrTestCsharp.Object;
using Antlr4.Runtime.Tree;
using System.Text.RegularExpressions;

namespace AntlrTestCsharp.parser
{
    public class FindVariableInMethod : CSharpParserBaseListener
    {
        CSharpParser parser;
        List<VariableDefine> listLocalVar = new List<VariableDefine>();
        //public List<string> listVal { get; set; }
        //public string varName { get; set; }
        public int varLine { get; set; }
        public bool isContain { get; set; }
        public List<TempExpression> listExpress { get; set; }

        public FindVariableInMethod(CSharpParser parser, List<TempExpression> listExpress)
        {
            this.parser = parser;
            //this.varName = varName;
            //this.varLine = varLine;
            this.listExpress = listExpress;
            this.isContain = false;
        }

        public override void EnterLocal_variable_declaration([NotNull] CSharpParser.Local_variable_declarationContext context)
        {
            //if (listVal == null)
            //{
            //    listVal = new List<string>();
            //}
            //string varId = "";
            //for (int i = 0; i < context.ChildCount; i++)
            //{
            //    IParseTree childTree = context.GetChild(i);
            //    if (childTree is CSharpParser.Local_variable_declaratorContext)
            //    {
            //        varId = childTree.GetChild(0).GetText();

            //        if (varId.Equals(varName))
            //        {
            //            listVal.Add(childTree.GetChild(2).GetText());
            //            isContain = true;
            //            break;
            //        }
            //    }
            //}
        }

        public override void EnterExpression([NotNull] CSharpParser.ExpressionContext context)
        {
            if (context.Start.Line >= listExpress[0].line)
            {
                return;
            }
            foreach(var item in listExpress)
            {
                if (isDefine(context.GetText(), context.Start.Line, item.value) && isStatic(context.GetText()))
                {
                    item.isVuln = false;
                }
                else if(isDefine(context.GetText(),context.Start.Line,item.value) && !isStatic(context.GetText()))
                {
                    item.isVuln = true;
                }

            }
            //if (isDefine(context.GetText(), context.Start.Line))
            //{
            //    isContain = true;
            //}
            //else
            //{
            //    isContain = false;
            //}

        }

        private bool isDefine(string expression, int index, string varName)
        {
            string pattern = varName + "\\s+=[^;]*|" + varName + "=[^;]*";
            Regex regex = new Regex(pattern);
            Match ma = regex.Match(expression);
            if (!string.IsNullOrEmpty(ma.ToString()))
            {
                return true;
            }
            else { return false; }
        }

        private bool isStatic(string input)
        {
            //int count = 0;
            if (input.Contains("\""))
            {
                input = input.Trim();
                int range = input.Length - input.IndexOf("=")-1;
                input = input.Substring(input.IndexOf("=")+1, range);
                if (input.Contains("+"))
                {
                    string[] part = input.Split('+');
                    foreach(var item in part)
                    {
                        if (!item.EndsWith("\"")) { return false; }
                    }
                }
                if (input.EndsWith("\"")) { return true; }
                return false;
            }
            else
            {
                return false;
            }
        }
        
    }
}
